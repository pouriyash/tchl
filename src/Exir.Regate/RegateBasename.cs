using System.Net;

namespace Exir.Regate
{
    public static class RegateBasename
    {
        public static string Build(string value = "", bool isRequired = false)
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='Basename'
                type='text'
                class='form-control'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}