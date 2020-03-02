using DNTPersianUtils.Core;

namespace Tochal.Core.Common.Extension
{
    public static class PriceExtensions
    {
        public static string TomansThousand(this decimal _price)
        {
            var priceToman = (_price / 10);
            var priceString = string.Format("{0:n0}", priceToman);
            var priceStringPersian = priceString.ToPersianNumbers();

            return priceStringPersian;
        }

        public static string Thousand(this decimal _price)
        {
            var priceString = string.Format("{0:n0}", _price);
            var priceStringPersian = priceString.ToPersianNumbers();

            return priceStringPersian;
        }
    }
}
