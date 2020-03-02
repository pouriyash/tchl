using Tochal.Core.Common.GuardToolkit;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks; 
using DNTCommon.Web.Core;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using SmsPanel;

namespace Tochal.Infrastructure.Services.Identity
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IOptionsSnapshot<SiteSettings> _smtpConfig;
        private readonly IWebMailService _webMailService;

        public AuthMessageSender(
            IOptionsSnapshot<SiteSettings> smtpConfig,
            IWebMailService webMailService)
        {
            _smtpConfig = smtpConfig;
            _smtpConfig.CheckArgumentIsNull(nameof(_smtpConfig));

            _webMailService = webMailService;
            _webMailService.CheckArgumentIsNull(nameof(webMailService));
        }

        public Task SendEmailAsync<T>(string email, string subject, string viewNameOrPath, T model)
        {
            return _webMailService.SendEmailAsync(
                _smtpConfig.Value.Smtp,
                new[] { new MailAddress { ToName = "", ToAddress = email } },
                subject,
                viewNameOrPath,
                model
            );
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return _webMailService.SendEmailAsync(
                _smtpConfig.Value.Smtp,
                new[] { new MailAddress { ToName = "", ToAddress = email } },
                subject,
                message
            );
        }

        public async Task<string> SendSmsAsync(string number, string message)
        {
            var userName = _smtpConfig.Value.smsConfig.UserName;
            var password = _smtpConfig.Value.smsConfig.Password;
                
            var client = new SMSWebServiceClient();
            var result = await client.SendSMSAuthAsync(userName, password, number, message);
            await client.CloseAsync();

            return result; 
            //return Task.FromResult(0);
        }
    }
}