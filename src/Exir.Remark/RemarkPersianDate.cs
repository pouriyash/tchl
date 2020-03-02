using System;
using System.Collections.Generic;
using System.Text;
using DNTPersianUtils.Core;

namespace Exir.Remark
{
    public static class RemarkPersianDate
    {
        public static string Build(DateTime? date)
        {
            if (date == null)
                return "";

            return ((DateTime) date).ToShortPersianDateString();
        }
    }
}
