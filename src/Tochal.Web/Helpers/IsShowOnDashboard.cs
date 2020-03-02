using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public class IsShowOnDashboard: Attribute
    {
        public bool IsShow { get; set; } = false;

        public IsShowOnDashboard(bool IsShow)
        {
            this.IsShow = IsShow;
        }
    }
}
