using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.SSOT
{
    public enum UserTypeSSOT
    {
        [DisplayName("شخص")]
        Person=0,

        [DisplayName("شرکت")]
        Company=1,

        [DisplayName("دانشگاه")]
        university = 2
    }
}
