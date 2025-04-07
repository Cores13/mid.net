using AbySalto.Mid.Domain.DTOs.Email;

namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailDataDto request, CancellationToken ct);

        Task<bool> SendVerificationCodeEmailAsync(List<string> to, string userName, string code);

        Task<bool> SendEmailVerificationCodeEmail(List<string> to, string userName, string code);
    }
}
