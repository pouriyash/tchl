using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public class Extentions
    {
    }

    public static class hhh
    {
        public static bool HasAttribute(this MemberInfo type, Type attribute, bool inherit = false)
        {
            return Attribute.IsDefined(type, attribute, inherit);
        }
    }

    
}
