using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Infrastructure.Services.Contracts
{
    public interface ISmsManagement
    {
        MessageSendResponseObject sendMessage(string phoneNumber, string message);
        MessageSendResponseObject sendMessages(List<string> phoneNumber, List<string> message);
    }
}
