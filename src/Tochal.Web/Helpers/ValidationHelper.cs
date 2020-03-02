using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsItNumber(this string text)
        {
            var isNumber = new Regex("[^0-9]");
            return !isNumber.IsMatch(text);
        }
         
    }
}
