using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.ResendVerificationEmail
{
    public sealed class ResendVerificationEmailCommandHandler : ICommandHandler<ResendVerificationEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeService _verificationCodeService;

        public ResendVerificationEmailCommandHandler(IUserRepository userRepository, IVerificationCodeService verificationCodeService)
        {
            _userRepository = userRepository;
            _verificationCodeService = verificationCodeService;
        }

        public async Task<Result> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user is null || user.EmailVerifiedAt is not null)
            {
                return Result.Failure<TokenResponseDto>(
                    DomainErrors.User.InvalidRequest);
            }

            await _verificationCodeService.CreateCodeAsync((int)user.Id, VerificationCodeType.EmailVerification, cancellationToken);
            
            return Result.Success();
        }
    }
}
