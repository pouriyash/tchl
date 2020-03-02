using Tochal.Core.DomainModels.Entity.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Tochal.Core.DomainModels.Entity.Identity
{
     
    public class UserClaim : IdentityUserClaim<int>, IAuditableEntity
    {
        public virtual User User { get; set; }
    }
}