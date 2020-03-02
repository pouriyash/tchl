using System.ComponentModel.DataAnnotations;

namespace Tochal.Core.DomainModels.SSOT
{

    public enum DegreeSSOT
    {
        [Display(Name = "کارشناسی")]
        Bachelor = 2,

        [Display(Name = "کارشناسی ارشد")]
        MasterOfScience = 4,

        [Display(Name = "دکتری")]
        Doctoral = 8,
    }
    
}