using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum RegisterTypeSSOT
    {
        [Display(Name ="دانشجو")]
        student=0,
        [Display(Name = "شرکت")]
        company=1,
        [Display(Name = "دانشگاه")]
        university = 2
    }
}
