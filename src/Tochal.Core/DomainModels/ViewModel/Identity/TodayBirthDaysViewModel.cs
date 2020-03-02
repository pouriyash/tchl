using System.Collections.Generic;
using Tochal.Core.DomainModels.ViewModel.Identity;
using Tochal.Core.DomainModels.Entity.Identity;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class TodayBirthDaysViewModel
    {
        public List<User> Users { set; get; }

        public AgeStatViewModel AgeStat { set; get; }
    }
}