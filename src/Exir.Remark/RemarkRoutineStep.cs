using System;
using System.Collections.Generic;
using System.Text;

namespace Exir.Remark
{
    public static class RemarkRoutineStep
    {
        public static string Build(int stepId, int routineId)
        {
            return $@"
                <span data-ui='StepId' data-step='{stepId}' data-routine={routineId}>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
            ";
        }
    }
}
