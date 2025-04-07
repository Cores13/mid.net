using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Exceptions;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly HttpContext _currentContext;
        public VerificationCodeService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _currentContext = httpContextAccessor.HttpContext;
        }

        public async Task<string> GetCodeAsync(int userId, VerificationCodeType type, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(DomainErrors.User.DoesNotExist.Message);
            }

            if (type == VerificationCodeType.PasswordReset)
            {
                if (user.ResetPasswordToken is null)
                {
                    throw new NotFoundException(DomainErrors.User.ResetPasswordTokenDoesNotExist.Message);
                }
                return user.ResetPasswordToken;
            }
            else
            {
                if (user.EmailVerificationToken is null)
                {
                    throw new NotFoundException(DomainErrors.User.EmailVerificationTokenDoesNotExist.Message);
                }
                return user.EmailVerificationToken;
            }
        }

        public async Task<bool> VerifyCodeAsync(int userId, string code, VerificationCodeType type, bool deleteIfValid, CancellationToken cancellationToken = default)
        {
            var dbCode = await GetCodeAsync(userId, type, cancellationToken);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (dbCode != null && code == dbCode.ToUpper())
            {
                if (deleteIfValid)
                {
                    switch (type)
                    {
                        case VerificationCodeType.PasswordReset:
                            user.ResetPasswordToken = null;
                            break;
                        case VerificationCodeType.EmailVerification:
                            user.EmailVerificationToken = null;
                            break;
                        default: break;
                    }
                }
                return true;
            }

            return false;
        }

        public async Task<string> CreateCodeAsync(int userId, VerificationCodeType type, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(DomainErrors.User.DoesNotExist.Message);
            }

            var newCode = GenerateCode();

            var to = new List<string> { user.Email };

            if (type == VerificationCodeType.PasswordReset)
            {
                user.ResetPasswordToken = newCode.ToUpper();
                user.ResetPasswordExpiry = DateTime.UtcNow.AddMinutes(15);
                _ = _emailService.SendVerificationCodeEmailAsync(to, user.Name, user.ResetPasswordToken);
            }
            else if (type == VerificationCodeType.EmailVerification)
            {
                user.EmailVerificationToken = newCode.ToUpper();
                user.EmailVerificationExpiry = DateTime.UtcNow.AddMinutes(15);

                //var request = _currentContext.Request;
                //var host = request.Host.ToUriComponent();
                //var pathBase = request.PathBase.ToUriComponent();
                //var code = $"{request.Scheme}://{host}{pathBase}/api/user/verifyemail/{user.Id}/{user.EmailVerificationToken}";
                
                var code = $"/verify-email/{user.Id}/{user.EmailVerificationToken}";
                _ = _emailService.SendEmailVerificationCodeEmail(to, user.Name, code);
            }

            return newCode;
        }

        private static string GenerateCode()
        {
            var length = 12;
            var random = new Random();

            const string pool = "abcdefghijklmnopqrstuvwxyz123456789";
            var chars = Enumerable.Range(0, length).Select(x => pool[random.Next(0, pool.Length)]);

            return new string(chars.ToArray());
        }
    }
}
