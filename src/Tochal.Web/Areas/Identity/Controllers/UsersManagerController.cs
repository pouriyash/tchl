using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.DomainModels.Entity.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tochal.Infrastructure.Services.Identity;
using Tochal.Infrastructure.Services.Contracts.Identity;
using System;
using System.ComponentModel;
using Tochal.Web.Helpers;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Core.DomainModels.ViewModel.Identity.Emails;
using Research.City.Admin.Toolkit;
using Alamut.Data.Structure;
using System.Linq;
using Tochal.Infrastructure.Services;
using AutoMapper;
using Newtonsoft.Json;
using Tochal.Core.DomainModels.DTO;
using Alamut.Data.Paging;

namespace Tochal.Web.Areas.Identity.Controllers
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(Title = " کاربران", UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName(" کاربران")]
    public class UsersManagerController : Controller
    {
        private const int DefaultPageSize = 7;

        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly UserService _userService;

        public UsersManagerController(
            IApplicationUserManager userManager,
            IApplicationRoleManager roleManager,
            IApplicationSignInManager signInManager,
            UserService UserService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));

            _userService = UserService;
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ActivateUserEmailStat(int userId)
        {
            User thisUser = null;
            var result = await _userManager.UpdateUserAndSecurityStampAsync(
                userId, user =>
                {
                    user.EmailConfirmed = true;
                    thisUser = user;
                });
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }
         
            return await returnUserCardPartialView(thisUser);
        }
        

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ChangeUserLockoutMode(int userId, bool activate)
        {
            User thisUser = null;
            var result = await _userManager.UpdateUserAndSecurityStampAsync(
                userId, user =>
                {
                    user.LockoutEnabled = activate;
                    thisUser = user;
                });
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }

            return await returnUserCardPartialView(thisUser);
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ChangeUserRoles(int userId, int[] roleIds)
        {
            User thisUser = null;
            var result = await _userManager.AddOrUpdateUserRolesAsync(
                userId, roleIds, user => thisUser = user);
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }
            return await returnUserCardPartialView(thisUser);
        }


        public async Task<IActionResult> ChangeUserStat(int userId, bool activate)
        {
            User thisUser = null;
            var result = await _userManager.UpdateUserAndSecurityStampAsync(
                userId, user =>
                        {
                            user.IsActive = activate;
                            thisUser = user;
                        });
            if (!result.Succeeded)
            {
                return Json(false);
                //return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }

            return Json(true);

            //return await returnUserCardPartialView(thisUser);
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ChangeUserTwoFactorAuthenticationStat(int userId, bool activate)
        {
            User thisUser = null;
            var result = await _userManager.UpdateUserAndSecurityStampAsync(
                userId, user =>
                {
                    user.TwoFactorEnabled = activate;
                    thisUser = user;
                });
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }

            return await returnUserCardPartialView(thisUser);
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> EndUserLockout(int userId)
        {
            User thisUser = null;
            var result = await _userManager.UpdateUserAndSecurityStampAsync(
                userId, user =>
                {
                    user.LockoutEnd = null;
                    thisUser = user;
                });
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }

            return await returnUserCardPartialView(thisUser);
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        [DisplayName(" کاربران")]
        [GroupingDashboard(GroupingDashboardSSOT.Management)]
         public async Task<IActionResult> Index(UserSearchViewModel vm)
        {
            //vm.ItemsPerPage = 20;
            //var model = await _userManager.GetPagedUsersListAsync(vm,
            //    showAllUsers: true,
            //    personFilter: personFilter,
            //    genderFilter: genderFilter,
            //    statusFilter: statusFilter,
            //    companyFilter: companyFilter,
            //    term);

            //model.CurrentPage = vm.CurrentPage;
            //model.ItemsPerPage = vm.ItemsPerPage;
            //model.ShowFirstLast = true;

            //if (HttpContext.Request.IsAjaxRequest())
            //{
            //    return PartialView("_UsersList", model);
            //}
            //ViewBag.CurrentPage = vm.CurrentPage;
            //return View(model);
            var users = _userService.GetUsers(vm);

            var TotalCount = await _userService.TotalCount();

            var model = new SearchCriteriaPageModel<IPaginated<User>, UserSearchViewModel,int>(users, vm, TotalCount);

            return View(model);
        }
        [AllowAnonymous]    
        public async Task<string> GetJson(PagedUsersListViewModel vm, string personFilter, string genderFilter, string statusFilter, string companyFilter, string term= "")
        {
            //vm.ItemsPerPage = 20;
            var model = await _userManager.GetPagedUsersListAsync(vm, showAllUsers: true, personFilter: personFilter,
                genderFilter: genderFilter,
                statusFilter: statusFilter,
                companyFilter: companyFilter,
                term: term);

            model.CurrentPage = vm.CurrentPage;
            model.ItemsPerPage = vm.ItemsPerPage;
            model.ShowFirstLast = true;
            var t = JsonConvert.SerializeObject(model);
            return t;
        }

        //[AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        //public async Task<IActionResult> SearchUsers(SearchUsersViewModel model)
        //{
        //    var pagedUsersList = await _userManager.GetPagedUsersListAsync(
        //        pageNumber: 0,
        //        model: model);

        //    pagedUsersList.Paging.CurrentPage = 1;
        //    pagedUsersList.Paging.ItemsPerPage = model.MaxNumberOfRows;
        //    pagedUsersList.Paging.ShowFirstLast = true;

        //    model.PagedUsersList = pagedUsersList;
        //    return PartialView("_SearchUsers", model);
        //}

        private async Task<IActionResult> returnUserCardPartialView(User thisUser)
        {
            var roles = await _roleManager.GetAllCustomRolesAsync();
            return PartialView(@"~/Areas/Identity/Views/UserCard/_UserCardItem.cshtml",
                new UserCardItemViewModel
                {
                    User = thisUser,
                    ShowAdminParts = true,
                    Roles = roles,
                    ActiveTab = UserCardItemActiveTab.UserAdmin
                });
        }

        [DisplayName("مدیریت نقش کاربر")]
        public async Task<IActionResult> roleManage(SearchUsersViewModel model)
        {

            model.IsUserId = true;
            //var model = await _userManager.GetUserAsync();
            var pagedUsersList = await _userManager.GetPagedUsersListAsync(
               pageNumber: 0,
               model: model);



            return PartialView(pagedUsersList);
        }

        [HttpGet]
        [DisplayName("ویرایش اطلاعات")]
        public IActionResult Edit(int id)
        {
            var record = _userManager.FindById(id);
            ViewBag.Id = id;
            return View("EditPerson", record);
            
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel vm)
        {
            
            var record =  _userService.EditUser(vm);
            TempData.AddResult(record);

           return Utility.CloseAndRefresh();
        }

        [HttpPost]
        [DisplayName("ویرایش")]
        public IActionResult Test()
        {

            return Utility.CloseAndRefresh();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(int userid)
        {
            var user = await _userManager.FindByIdAsync(userid.ToString());
            await _signInManager.SignInAsync(user, false, null);
            return Redirect("/");
        }
         
    }
}