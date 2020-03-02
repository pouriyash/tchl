using System;
using System.Threading.Tasks;
using Alamut.Service.Messaging;

namespace Service.Messaging.Contracts
{
    public interface IEmailService
    {
        void SendEmail<TMailId>(Service.Messaging.Configration.EmailBody<TMailId> body,
            Action<TMailId> onSuccess = null) where TMailId : struct;
    }
}
