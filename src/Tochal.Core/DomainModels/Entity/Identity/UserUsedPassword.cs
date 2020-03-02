

using Tochal.Core.DomainModels.Entity.AuditableEntity;

namespace Tochal.Core.DomainModels.Entity.Identity
{ 
    public class UserUsedPassword : IAuditableEntity
    {
        public int Id { get; set; }

        public string HashedPassword { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}