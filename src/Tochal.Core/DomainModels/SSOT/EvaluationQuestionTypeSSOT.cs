using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum EvaluationQuestionTypeSSOT
    {
        [Display(Name = "کارآموز")]
        Intern = 1,

        [Display(Name = "کارشناس")]
        Expert = 2
    }
}
