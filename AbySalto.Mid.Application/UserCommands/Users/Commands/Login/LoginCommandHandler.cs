using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Application.Abstractions;
using AbySalto.Mid.Domain.Interfaces.Services;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Login
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, TokenResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            IAuthService authService,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _authService = authService;
            _passwordService = passwordService;
        }

        public async Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.InvalidCredentials);
            }

            if (!_passwordService.VerifyPassword(user, request.Password))
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.InvalidCredentials);
            }

            if (user.EmailVerifiedAt == null)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.UserNotVerified);
            }

            if (user.Status == UserStatusEnum.Inactive)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.UserInactive);
            }

            string token = _jwtProvider.Generate(user);
            var newRefreshToken = _authService.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);


            return Result.Success(new TokenResponseDto()
            {
                AccessToken = token,
                RefreshToken = newRefreshToken
            });
        }
    }
}
