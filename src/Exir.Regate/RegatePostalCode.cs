using System.Net;

namespace Exir.Regate
{
    public static class RegatePostalCode
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='{name}'
                type='text'
                class='form-control'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
