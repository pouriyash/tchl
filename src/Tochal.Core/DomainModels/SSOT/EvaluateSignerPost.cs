using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum EvaluateSignerPost
    {
        [Display(Name = "مدیر عامل")]
        Manager = 1,

        [Display(Name = "سرپرست کارآموزی")]
        TochalGuardian = 2,

        [Display(Name = "سایر")]
        Other = 3,
    }
}
