using RazorLight;
using RazorLight.Extensions;
using System;
using System.Linq;
using System.Net;




namespace Tochal.Core.Common.Extension
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string input)
        {
            if (input == null) return "";

            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        public static string SortString(this string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        /*
        public static string CompileRazor(this string input, object model)
        {
            var engine = EngineFactory.CreatePhysical(@"c:\Windows");
            string result = engine.ParseString(input, model);
            return result;
        }
        */

        public static string CompileRazor<T>(this string input, T model)
        {
            var engine = EngineFactory.CreatePhysical(@"c:\Windows");
            string result = engine.ParseString(input, model);

            result = WebUtility.HtmlDecode(result);

            return result;
        }

        public static string ShowTextOrWhiteSpace(this string input, int count = 12)
        {
            var underlines = "";
            for (var i = 0; i < count; i++)
            {
                underlines += "_";
            }

            return input ?? underlines;
        }

        public static string ShowFriendlyGender(this bool? isMale
            , string maleText = "جناب آقای"
            , string femaleText = "سرکار خانم"
            , string nullText = ""
        )
        {
            if (!isMale.HasValue)
                return nullText;

            return Convert.ToBoolean(isMale) ? maleText : femaleText;
        }

    }
}
