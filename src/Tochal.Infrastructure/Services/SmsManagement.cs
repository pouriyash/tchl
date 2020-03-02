using Tochal.Infrastructure.Services.Contracts;
using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Infrastructure.Services
{

    public class SmsManagement : ISmsManagement
    {

        public MessageSendResponseObject sendMessage(string phoneNumber, string message)
        {

            var result = sendMessages(new List<string> { phoneNumber }, new List<string> { message });

            return result;

        }

        public MessageSendResponseObject sendMessages(List<string> phoneNumber, List<string> message)
        {

            var token = new Token().GetToken("f1441cad71e7fcc3c7561cad", "cd691dec0b70eca37981d5");

            var messageSendObject = new MessageSendObject()
            {
                Messages = message.ToArray(),
                MobileNumbers = phoneNumber.ToArray(),
                LineNumber = "30004505000207",
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

            return messageSendResponseObject;

        }
    }

}
