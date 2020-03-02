using AutoMapper.QueryableExtensions;
using Tochal.Core.DomainModels.DTO.User;
using Tochal.Core.DomainModels.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tochal.Core.DomainModels.SSOT;
using AutoMapper; 
using Tochal.Core.DomainModels.ViewModel.Identity;
using Alamut.Data.Structure;
using Tochal.Core.DomainModels.DTO;
using Alamut.Data.Paging;
using Alamut.Data.Linq;
using Alamut.Helpers.Linq;

namespace Tochal.Infrastructure.Services
{
    public class UserService
    {
        private readonly DataLayer.Context.IUnitOfWork _context;
        private DbSet<User> Users = null;
        #region constructor
        public UserService(DataLayer.Context.IUnitOfWork context)
        {
            _context = context;
            Users = _context.Set<User>();
        }
        #endregion

        public List<UserSummaryTiny> FindUserByNameOrNationalCode(string searchTerm)
        {
            return Users
                .Where(x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm) || x.NationalCode.Contains(searchTerm))
                .Take(20)
                .OrderBy(x => x.DisplayName)
                .ProjectTo<UserSummaryTiny>()
                .ToList();
        }

        public async Task<bool> FindUserByUserName(string userName)
        {
            return await Users
                .AnyAsync(x => x.UserName.Contains(userName));
        }

        public bool FindUserByNationalCode(string NationalCode)
        {
            return Users
                .Any(x => x.NationalCode.Contains(NationalCode));
        }


        public UserSummaryTiny GetByUserName(string userName)
        {
            return Users
                .Where(p => p.UserName == userName)
                .ProjectTo<UserSummaryTiny>()
                .FirstOrDefault();
        }

        public async Task<bool> FindUserByEmail(string email)
        {
            return await Users
                .AnyAsync(x => x.Email.Contains(email));
        }

        

        public User LastUser()
        {
            return Users.LastOrDefault();
        }

        public string GenerateUserName()
        {
            var lastUserId = LastUser().Id;
            var userNameTemplate = DateTime.Now.ToPersianYearMonthDay().ToString().Replace("/", "");
            return userNameTemplate + lastUserId;
        }

        public User GetUserById(int id)
        {
            return Users 
                .FirstOrDefault(q => q.Id == id);
        }

        public List<User> GetUniversityNames(int id)
        {
            return Users.ToList();
        }

        //public void UpdateCompanyCapacities(UserCompanyEditViewModel vm)
        //{
        //    var user = Users.Find(vm.Id);

        //    Mapper.Map(vm, user);

        //    Users.Update(user);
        //    _context.SaveChanges();
        //}

        public void UpdateUser(EditUserInfoViewModel vm)
        {
            var user = Users.Find(vm.Id);

            Mapper.Map(vm, user);

            Users.Update(user);
            _context.SaveChanges();
        }
         
        public ServiceResult EditUser(UserEditViewModel vm)
        {
            var user = Users.Find(vm.Id);
            if (user == null)
                return ServiceResult.Error();

            Mapper.Map(vm, user);

            Users.Update(user);
            _context.SaveChanges();

            return ServiceResult.Okay();

        }

        public List<IdName> GetIdNamesByids(List<int> ids)
        {
            return Users
                .Where(q => ids.Contains(q.Id))
                .Select(s => new IdName
                {
                    Id = s.Id,
                    Name = s.FirstName + " " + s.LastName
                })
                .ToList();
        }

        public void UpdateUser(User user)
        {
            var record = Users.Find(user.Id);

            Mapper.Map(user, record);

            Users.Update(record);

            _context.SaveChanges();

        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return Users.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<int> TotalCount()
        {
            return await Users.CountAsync();
        }

        public IPaginated<User> GetUsers(UserSearchViewModel vm)
        {
            return Users
                 .WhereIf(vm.Gender != null, u => u.Gender == vm.Gender)
                 .ToPaginated(new PaginatedCriteria(vm.Page, vm.PageSize));
        }

        

        public User GetUserByNationalCode(string NationalCode)
        {
            return Users
                .FirstOrDefault(u => u.NationalCode.Trim().Equals(NationalCode.Trim()));
        }

        public int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
