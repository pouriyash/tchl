using System.Threading.Tasks;

namespace Tochal.Infrastructure.Services.Contracts.Identity
{
    public interface ISmsSender
    {
        #region BaseClass

        Task<string> SendSmsAsync(string number, string message);

        #endregion

        #region CustomMethods

        #endregion
    }
}