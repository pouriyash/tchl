using Tochal.Core.DomainModels.Entity.Identity;

namespace Tochal.Core.DomainModels.ViewModel.Identity.Emails
{
    public class ChangePasswordNotificationViewModel : EmailsBase
    {
        public User User { set; get; }
    }
}