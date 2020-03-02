using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkSubSystemType
    {
        public static string Build(int? value = null)
        {
            if (value == null)
                return "";

            return $@"
                <span data-ui='RemarkSubSystemType' data-id='{value}'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
