using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Exir.Regate
{
    public static class RegateTitle
    {
        public static string Build(string value = "", bool isRequired = true)
        {
            value = WebUtility.HtmlEncode(value);

            return $@"<input
                name='Title'
                type='text'
                class='form-control'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
