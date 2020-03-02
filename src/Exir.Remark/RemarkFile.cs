using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkFile
    {
        public static string Build(string fileUrl, string webAddress)
        {
            var hasTrailingSlash = webAddress.EndsWith("/");
            var url = $"{webAddress}{(hasTrailingSlash ? "" : "/")}{fileUrl}";

            return url;
        }
    }
}
