using AbySalto.Mid.Infrastructure.Options.Email;
using Microsoft.Extensions.Options;

namespace AbySalto.Mid.WebApi.OptionsSetup
{
    public class EmailOptionsSetup : IConfigureOptions<EmailOptions>
    {
        private const string SectionName = "MailSettings";
        public readonly IConfiguration _configuration;
        public EmailOptionsSetup(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public void Configure(EmailOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
