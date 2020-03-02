using Tochal.Core.DomainModels.Entity.Identity;

namespace Tochal.Core.DomainModels.ViewModel.Identity.Emails
{
    public class RegisterEmailConfirmationViewModel : EmailsBase
    {
        public User User { set; get; }
        public string EmailConfirmationToken { set; get; }
    }
}
