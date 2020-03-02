using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkForeignKey
    {
        public static string Build(string url, int? value = null)
        {
            if (value == null)
                return "";

            return $@"
                <span data-ui='RemarkForeignKey' data-id='{value}' data-url='{url}'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
