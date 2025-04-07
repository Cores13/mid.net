using AbySalto.Mid.Domain.Enums;

namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IVerificationCodeService
    {
        Task<string> GetCodeAsync(int userId, VerificationCodeType type, CancellationToken cancellationToken = default);
        Task<bool> VerifyCodeAsync(int userId, string code, VerificationCodeType type, bool deleteIfValid = false, CancellationToken cancellationToken = default);
        Task<string> CreateCodeAsync(int userId, VerificationCodeType type, CancellationToken cancellationToken = default);
    }
}
