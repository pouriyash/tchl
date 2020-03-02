using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum IntroductionSignerPost
    {
        [Display(Name = "مدیر ارتباط با صنعت")]
        IndustryRelationsManager = 1,

        [Display(Name = "مدیر آموزش")]
        TrainingManager = 2,

        [Display(Name = "معاون پژوهشی")]
        ResearchAssistant = 3,

        [Display(Name = "سایر")]
        Others = 4,



    }
}
