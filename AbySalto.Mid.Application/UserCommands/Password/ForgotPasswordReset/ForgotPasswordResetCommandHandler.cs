using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordReset;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace Authentication.Application.UserCommands.Password.ForgotPasswordReset
{
    public sealed class ForgotPasswordResetCommandHandler : ICommandHandler<ForgotPasswordResetCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IPasswordService _passwordService;

        public ForgotPasswordResetCommandHandler(IPasswordService passwordService, IUserRepository userRepository, IVerificationCodeService verificationCodeService)
        {
            _userRepository = userRepository;
            _verificationCodeService = verificationCodeService;
            _passwordService = passwordService;
        }

        public async Task<Result> Handle(ForgotPasswordResetCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure(
                    DomainErrors.User.DoesNotExist);
            }

            var isCodeValid = await _verificationCodeService.VerifyCodeAsync((int)user.Id, request.Code, VerificationCodeType.PasswordReset, true, cancellationToken);

            if (!isCodeValid)
            {
                return Result.Failure(
                    DomainErrors.VerificationCodes.InvalidOrExpiredCode);
            }

            byte[] passwordHash, passwordSalt;
            _passwordService.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Result.Success();
        }
    }
}
