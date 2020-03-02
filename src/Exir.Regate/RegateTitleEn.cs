using System.Net;

namespace Exir.Regate
{
    public static class RegateTitleEn
    {
        public static string Build(string value = "", bool isRequired = true)
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='TitleEn'
                type='text'
                class='form-control'
                value='{value}'
                dir='ltr'
                style='direction: ltr; text-align: left;'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
