using System.ComponentModel.DataAnnotations;

namespace Tochal.Core.DomainModels.SSOT
{

    public enum ManageDashboard
    {
        [Display(Name = "متقاضی")]
        Applicant = 0,

        [Display(Name = "مدیر")]
        manager = 1,

        [Display(Name = "دبیرخانه")]
        sctriant = 2,
    }
    
}