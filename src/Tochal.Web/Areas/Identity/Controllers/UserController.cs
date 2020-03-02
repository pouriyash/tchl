using System.Threading.Tasks;
using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;

using Tochal.Core.DomainModels.ViewModel.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Tochal.Infrastructure.Services.Identity;
using Tochal.Infrastructure.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Research.City.Admin.Toolkit;
using Alamut.Data.Structure;
using System.ComponentModel;
using Tochal.Web.Helpers;
using Tochal.Core.DomainModels.SSOT;
using System.Collections.Generic;

namespace Tochal.Web.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [Area(AreaConstants.IdentityArea)]
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    public class UserController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(
            IApplicationUserManager userManager,
            IHttpContextAccessor contextAccessor,
        IApplicationRoleManager roleManager,
            UserService userService)
        {
            _contextAccessor = contextAccessor;
            _userService = userService;
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));
        }

        public IActionResult SelectUser(string field)
        {
            ViewBag.Field = field;
            return View();
        }

        [HttpPost]
        public IActionResult SelectUserPost(string searchTerm)
        {
            var model = _userService.FindUserByNameOrNationalCode(searchTerm);
            return Json(model);
        }
          
        public IActionResult GetById(int id)
        {
            var model = _userService.GetIdNamesByids(new List<int> { id })[0];

            return Json(new
            {
                Id = model.Id,
                Title = model.Name
            });
        }
    }
}