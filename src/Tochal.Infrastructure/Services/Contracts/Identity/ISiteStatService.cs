using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Tochal.Core.DomainModels.Entity.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity;

namespace Tochal.Infrastructure.Services.Contracts.Identity
{
    public interface ISiteStatService
    {
        Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

        Task<List<User>> GetTodayBirthdayListAsync();

        Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

        Task<AgeStatViewModel> GetUsersAverageAge();
    }
}