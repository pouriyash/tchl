using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkScienceGroupId
    {
        public static string Build(int? scienceGroupId)
        {
            if (scienceGroupId == null)
                return "";

            return $@"
                <span data-ui='ScienceGroupId' data-id='{scienceGroupId}' data-replace='Title'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
