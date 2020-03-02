using System.Collections.Generic;
using Tochal.Core.DomainModels.Entity.Identity;
using Tochal.Core.DomainModels.Entity;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class PagedUsersListViewModel: PageModel
    { 

        public int? Id { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }
        public List<User> Users { get; set; }

        public List<Role> Roles { get; set; }

    }
}
