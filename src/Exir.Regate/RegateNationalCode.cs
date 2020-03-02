using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Exir.Regate
{
    public static class RegateNationalCode
    {
        // gist.github.com/ebraminio/5292017
        private static bool IsValidIranianNationalCode(string input)
        {
            if (!Regex.IsMatch(input, @"^\d{10}$"))
                return false;

            var check = Convert.ToInt32(input.Substring(9, 1));
            var sum = Enumerable.Range(0, 9)
                          .Select(x => Convert.ToInt32(input.Substring(x, 1)) * (10 - x))
                          .Sum() % 11;

            return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        }

        public static string Build(string name = "", string value = "", bool isRequired = true, string placeholder = "")
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='{name}'
                type='text'
                class='form-control'
                placeholder='{placeholder}'
                value ='{value}'
                pattern='[0-9]{{10}}'
                dir='ltr'
                style='direction: ltr;'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
