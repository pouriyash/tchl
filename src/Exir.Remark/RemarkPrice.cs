using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkPrice
    {
        public static string Build(decimal? price
            , bool convertToPersianNumbers = true) =>
            price == null
                ? ""
                : Build((decimal)price);

        public static string Build(decimal price
            , bool convertToPersianNumbers = true)
        {
            var priceString = string.Format("{0:n0}", price);

            if (convertToPersianNumbers)
                priceString = priceString.ToPersianNumbers();

            return priceString;
        }
    }
}
