using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using Service.Messaging.Configration;
using Service.Messaging.Contracts;

namespace Alamut.Service.Messaging.Implementation
{
    public class MimeKitEmailServices : IEmailService
    {
        private static EmailConfig _emailConfig;
        private readonly ILogger<MimeKitEmailServices> _logger;

        public MimeKitEmailServices(EmailConfig emailConfig, ILogger<MimeKitEmailServices> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }


        public void SendEmail<TMailId>(EmailBody<TMailId> body, Action<TMailId> onSuccess = null)
            where TMailId : struct
            => Send(body, _emailConfig, _logger, onSuccess);

        private static void Send<TMailId>(EmailBody<TMailId> email,
            EmailConfig config,
            ILogger logger,
            Action<TMailId> onSuccess = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.Name, config.From));
            message.To.Add(new MailboxAddress(email.To));
            message.Subject = email.Subject;

            message.Body = new TextPart("html")
            {
                Text = email.Body
            };

            using (var client = new SmtpClient())
            {
                if (_emailConfig.SSL)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(config.Server, config.Port, SecureSocketOptions.None);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(_emailConfig.Password))
                {
                    client.Authenticate(config.From, config.Password);
                }

                client.Send(message);

                client.Disconnect(true);
            }

            onSuccess?.Invoke(email.Id);
        }
    }
}
