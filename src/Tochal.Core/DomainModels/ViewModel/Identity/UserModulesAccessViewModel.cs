using DNTCommon.Web.Core;
using Tochal.Core.DomainModels.DTO;
using Tochal.Core.DomainModels.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class UserModulesAccessViewModel
    {
        public ICollection<RoleClaim> RoleClaims { set; get; }

        public List<DashboardMenu> SecuredControllerActions { set; get; }

        public bool IsAdmin { get; set; }
    }
}
