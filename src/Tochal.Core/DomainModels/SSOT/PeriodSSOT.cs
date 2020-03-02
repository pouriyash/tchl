using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum PeriodSSOT
    {
        [Display(Name ="دوره اول")]
        first,
        [Display(Name = "دوره دوم")]
        secend,
        [Display(Name = "دوره سوم")]
        theird
    }
}
