using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordVerifyCode
{
    public sealed class ForgotPasswordVerifyCodeCommandHandler : ICommandHandler<ForgotPasswordVerifyCodeCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeService _verificationCodeService;

        public ForgotPasswordVerifyCodeCommandHandler(IUserRepository userRepository, IVerificationCodeService verificationCodeService)
        {
            _userRepository = userRepository;
            _verificationCodeService = verificationCodeService;
        }

        public async Task<Result> Handle(ForgotPasswordVerifyCodeCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure(
                    DomainErrors.User.DoesNotExist);
            }

            var isCodeValid = await _verificationCodeService.VerifyCodeAsync((int)user.Id, request.Code, VerificationCodeType.PasswordReset, false, cancellationToken);

            if (!isCodeValid)
            {
                return Result.Failure(
                    DomainErrors.VerificationCodes.InvalidOrExpiredCode);
            }

            return Result.Success();
        }
    }
}
