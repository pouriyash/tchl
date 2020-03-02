using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkPersianNumber
    {
        private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public static string Build(int number)
        {
            var chash = number.ToString();

            for (var i = 0; i < 10; i++)
                chash = chash.Replace(en[i], pn[i]);

            return chash;
        }
    }
}
