using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public class MenuFilterAttribute : Attribute
    {
        public bool IsShow { get; set; } = false;

        public MenuFilterAttribute()
        {
            this.IsShow = true;
        }
    }
}
