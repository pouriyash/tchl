using System.Net;

namespace Exir.Regate
{
    public static class RegateMobile
    {
        public static string Build(string name = "", string value = "", bool isRequired = false, string placeholder = "")
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='{name}'
                type='text'
                class='form-control'
                placeholder='{placeholder}'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
