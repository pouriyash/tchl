using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkLanguangeFlag
    {
        public static string Build(string language)
        {
            if (language == null)
                return "";

            return $"<img src='/theme/assets/images/flags/{language}.png' width='16' height='11' alt='{language}' />";
        }
    }
}
