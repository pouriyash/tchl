using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkFacultyId
    {
        public static string Build(int? facultyId)
        {
            if (facultyId == null)
                return "";

            return $@"
                <span data-ui='FacultyId' data-id='{facultyId}' data-replace='Title'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
