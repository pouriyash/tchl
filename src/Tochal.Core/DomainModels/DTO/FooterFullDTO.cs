using System;
using System.Collections.Generic;
using System.Text;
using Tochal.Core.DomainModels.Entity;

namespace Tochal.Core.DomainModels.DTO
{
    public class FooterFullDTO
    {
        public FooterInfo FooterInfo { get; set; }

        public List<FooterColleague> FooterColleague { get; set; }

    }
}
