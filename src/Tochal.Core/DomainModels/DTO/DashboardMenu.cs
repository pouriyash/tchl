using DNTCommon.Web.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.DTO
{
    public class DashboardMenu
    {
        public string Key{ get; set; } 
        public MvcActionViewModel Actions{ get; set; }
    }
}
