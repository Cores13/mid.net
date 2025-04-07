using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;
using System.Text;
using AbySalto.Mid.Infrastructure.Options.Email;
using AbySalto.Mid.Domain.Interfaces.Services;
using AbySalto.Mid.Domain.DTOs.Email;
using AbySalto.Mid.Domain.Templates;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _options;

        // New Email
        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task<bool> SendEmailAsync(MailDataDto request, CancellationToken ct = default)
        {
            try
            {
                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(_options.DisplayName, request.From ?? _options.From));
                mail.Sender = new MailboxAddress(request.DisplayName ?? _options.DisplayName, request.From ?? _options.From);

                // Receiver
                foreach (string mailAddress in request.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if (!string.IsNullOrEmpty(request.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(request.ReplyToName, request.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                if (request.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in request.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                // CC
                // Check if a CC address was supplied in the request
                if (request.Cc != null)
                {
                    foreach (string mailAddress in request.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = request.Subject;
                body.HtmlBody = request.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (_options.UseSSL)
                {
                    await smtp.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.SslOnConnect, ct);

                }
                else if (_options.UseStartTls)
                {
                    await smtp.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls, ct);
                }
                await smtp.AuthenticateAsync(_options.UserName, _options.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendVerificationCodeEmailAsync(List<string> to, string userName, string code)
        {
            try
            {
                var subject = "Verification code";
                var html = GetTemplateHtml("VerificationCode.cshtml", new VerificationCodeEmailDto { UserName = userName, Code = code.ToUpper() });

                var emailRequest = new MailDataDto(
                    to: to,
                    subject: subject,
                    body: html
                );
                var response = await SendEmailAsync(emailRequest, new CancellationToken());
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendEmailVerificationCodeEmail(List<string> to, string userName, string code)
        {
            try
            {
                var subject = "Verify email";
                code = string.Concat(_options.FrontendUrl, code);
                var html = GetTemplateHtml("EmailVerificationCode.cshtml", new VerificationCodeEmailDto { UserName = userName, Code = code.ToUpper() });

                var emailRequest = new MailDataDto(
                    to: to,
                    subject: subject,
                    body: html
                );
                var response = await SendEmailAsync(emailRequest, new CancellationToken());
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // New mail
        public string GetTemplateHtml(string emailTemplate, object? emailTemplateModel)
        {
            try
            {
                string mailTemplate = LoadTemplate(emailTemplate);

                IRazorEngine razorEngine = new RazorEngineCore.RazorEngine();
                IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

                return modifiedMailTemplate.Run(emailTemplateModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoadTemplate(string emailTemplate)
        {
            //string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //string templateDir = Path.Combine(baseDir, "Email/Templates");
            var templateDir = EmailTemplates.GetEmailTemplatesPath(AppDomain.CurrentDomain.BaseDirectory.ToString());

            string templatePath = Path.Combine(templateDir, $"{emailTemplate}");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}
