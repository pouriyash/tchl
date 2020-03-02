using System.Collections.Generic;
using Tochal.Core.DomainModels.Entity.Identity;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class OnlineUsersViewModel
    {
        public List<User> Users { set; get; }
        public int NumbersToTake { set; get; }
        public int MinutesToTake { set; get; }
        public bool ShowMoreItemsLink { set; get; }
    }
}