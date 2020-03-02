using System.ComponentModel.DataAnnotations; 

namespace Tochal.Core.DomainModels.SSOT
{
    public enum GenderType
    {
        [Display(Name = "زن")]
        female = 0,
        [Display(Name = "مرد")]
        male = 1

    }
}
