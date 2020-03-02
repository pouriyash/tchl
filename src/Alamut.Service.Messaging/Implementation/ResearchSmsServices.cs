using System;
using System.Threading.Tasks;
using Alamut.Service.Messaging.Configration;
using Alamut.Service.Messaging.Contracts;
using Microsoft.Extensions.Logging;
using SmsPanel;

namespace Alamut.Service.Messaging.Implementation
{
    public class ResearchSmsServices : ISmsService
    {
        private static SmsConfig _smsConfig;
        private readonly ILogger<ResearchSmsServices> _logger;

        public ResearchSmsServices(SmsConfig smsConfig, ILogger<ResearchSmsServices> logger)
        {
            _smsConfig = smsConfig;
            _logger = logger;
        }

        public async Task<string> SendSmsAsync(string number, string message)
        {
            var userName = _smsConfig.UserName;
            var password = _smsConfig.Password;

            var client = new SMSWebServiceClient();
            var result = await client.SendSMSAuthAsync(userName, password, number, message);
            await client.CloseAsync();

            return result;


        }
    }
}
