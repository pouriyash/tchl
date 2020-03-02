
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.DomainModels.ViewModel.Identity;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Tochal.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tochal.Web.Areas.Identity.Controllers
{

    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(Title = "مدیریت نقش‌های پویا", UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("مدیریت نقش‌های پویا")]

    public class DynamicRoleClaimsManagerController : Controller
    {
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
        private readonly IApplicationRoleManager _roleManager;

        public DynamicRoleClaimsManagerController(
            IMvcActionsDiscoveryService mvcActionsDiscoveryService,
            IApplicationRoleManager roleManager)
        {
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _mvcActionsDiscoveryService.CheckArgumentIsNull(nameof(_mvcActionsDiscoveryService));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        [DisplayName("ایندکس")]
        public async Task<IActionResult> Index(int? id)
        {
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "مدیریت نقش‌ها",
                Url = Url.Action("Index", "RolesManager"),
                Order = -1
            });

            if (!id.HasValue)
            {
                return View("Error");
            }

            var role = await _roleManager.FindRoleIncludeRoleClaimsAsync(id.Value);
            if (role == null)
            {
                return View("NotFound");
            }

            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            return View(model: new DynamicRoleClaimsManagerViewModel
            {
                SecuredControllerActions = securedControllerActions,
                RoleIncludeRoleClaims = role
            });
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(DynamicRoleClaimsManagerViewModel model)
        {
            var result = await _roleManager.AddOrUpdateRoleClaimsAsync(
                roleId: model.RoleId,
                roleClaimType: ConstantPolicies.DynamicPermissionClaimType,
                selectedRoleClaimValues: model.ActionIds);
            if (!result.Succeeded)
            {
                return BadRequest(error: result.DumpErrors(useHtmlNewLine: true));
            }
            return Json(new { success = true });
        }
    }
}