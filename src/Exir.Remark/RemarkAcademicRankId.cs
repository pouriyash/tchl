using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkAcademicRankId
    {
        public static string Build(int? academicRankId)
        {
            if (academicRankId == null)
                return "";

            return $@"
                <span data-ui='AcademicRankId' data-id='{academicRankId}' data-replace='Title'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
