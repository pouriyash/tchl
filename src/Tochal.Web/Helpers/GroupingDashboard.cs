using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public class GroupingDashboard : System.Attribute
    {
        public string _DisplayName { get; set; } 
        
        public GroupingDashboard(string DisplayName)
        {
            this._DisplayName = DisplayName;
        }
    }
}