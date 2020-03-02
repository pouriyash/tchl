using System.Threading.Tasks;

namespace Alamut.Service.Messaging.Contracts
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(string number, string message);
    }
}
