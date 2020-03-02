using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkUserId
    {
        public static string Build(int? userId)
        {
            if (userId == null)
                return "<span style='font-family: courier new;font-size: 12px;position: relative;top: -4px;'>_ _ _</span>";

            return $@"
                <span data-ui='UserId' data-id='{userId}' data-replace='Name'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
