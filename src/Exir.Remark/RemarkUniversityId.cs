using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkUniversityId
    {
        public static string Build(int? universityId)
        {
            if (universityId == null)
                return "";

            return $@"
                <span data-ui='UniversityId' data-id='{universityId}' data-replace='Title'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
