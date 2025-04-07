using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Application.Abstractions;
using AbySalto.Mid.Domain.Interfaces.Services;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.RenewToken
{
    internal sealed class RenewTokenCommandHandler : ICommandHandler<RenewTokenCommand, TokenResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IAuthService _authService;

        public RenewTokenCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IAuthService authService)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _authService = authService;
        }

        public async Task<Result<TokenResponseDto>> Handle(RenewTokenCommand request, CancellationToken cancellationToken = default)
        {
            string refreshToken = request.RefreshToken;

            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

            if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.InvalidRequest);
            }

            if (user.EmailVerifiedAt is null)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.UserNotVerified);
            }

            if (user.Status == UserStatusEnum.Inactive)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.UserInactive);
            }

            var newAccessToken = _jwtProvider.Generate(user);
            var newRefreshToken = _authService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

            return Result.Success(new TokenResponseDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
