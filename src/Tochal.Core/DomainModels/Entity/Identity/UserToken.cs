using Tochal.Core.DomainModels.Entity.AuditableEntity;

using Microsoft.AspNetCore.Identity;

namespace Tochal.Core.DomainModels.Entity.Identity
{
    
    public class UserToken : IdentityUserToken<int>, IAuditableEntity
    {
        public virtual User User { get; set; }
    }
}