﻿using System.Collections.Generic;
using Tochal.Core.DomainModels.Entity.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Tochal.Core.DomainModels.Entity.Identity
{
     
    public class Role : IdentityRole<int>, IAuditableEntity
    {
        public Role()
        {
        }

        public Role(string name)
            : this()
        {
            Name = name;
        }

        public Role(string name, string description)
            : this(name)
        {
            Description = description;
        }

        public string Description { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }

        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}