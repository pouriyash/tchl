using System.ComponentModel;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tochal.Infrastructure.Services.Identity;
using Tochal.Core.Common.IdentityToolkit;
using DNTCommon.Web.Core;
using Tochal.Core.DomainModels.ViewModel.Identity;
using Tochal.Infrastructure.Services.Contracts.Identity;
using System;
using System.Collections.Generic;
using Tochal.Core.DomainModels.Entity.Identity;
using System.Linq;
using Tochal.Web.Helpers;
using Tochal.Core.DomainModels.DTO;
using Tochal.Infrastructure.Services.Contracts;

namespace Tochal.Web.Controllers
{
    [Authorize]
    [DisplayName("خانه")]
    [BreadCrumb(Title = "خانه", UseDefaultRouteUrl = true, Order = 0)]
    public class HomeController : Controller
    {
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ISmsManagement _smsManagement;

        public HomeController(IMvcActionsDiscoveryService mvcActionsDiscoveryService, IApplicationRoleManager roleManager, ISmsManagement smsManagement)
        {
            _roleManager = roleManager;
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _smsManagement = smsManagement;
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        public IActionResult Index()
        {
            //_smsManagement.sendMessage("09302490811", "خوش آمدید.");
            var userId = User.Identity.GetUserId();
            var IsAdmin = User.IsInRole(ConstantRoles.Admin);

            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission).Select(p => p.MvcActions);
            var list = new List<MvcActionViewModel>();

            foreach (var item in securedControllerActions)
            {
                list.AddRange(item);
            }

            var filteraction = new List<DashboardMenu>();

            foreach (var item in list)
            {

                foreach (var item2 in item.ActionAttributes)
                {

                    var attr = item2 as GroupingDashboard;

                    if (attr != null)
                    {
                        filteraction.Add(new DashboardMenu
                        {
                            Key = attr._DisplayName,
                            Actions = item
                        });
                    }

                }

            }

            filteraction.Distinct();



            List<RoleClaim> userRoles = null;

            if (!IsAdmin)
            {
                userRoles = _roleManager.FindRoleClaimOfCurrentUser();
            }

            return View(model: new UserModulesAccessViewModel
            {
                SecuredControllerActions = filteraction,
                RoleClaims = userRoles,
                IsAdmin = IsAdmin
            });
        }

        [BreadCrumb(Title = "خطا", Order = 1)]
        public IActionResult Error()
        {
            return View();
        }

        [BreadCrumb(Title = "اکشن تستی 1", Order = 2)]
        [DisplayName("اکشن تستی 1")]
        public IActionResult Test1()
        {
            return View();
        }

        [BreadCrumb(Title = "اکشن تستی ۳", Order = 3)]
        [DisplayName("اکشن تستی 2")]
        public IActionResult Test2()
        {
            return View();
        }


        public IActionResult LiveReport()
        {
            return View();
        }

        /// <summary>
        /// To test automatic challenge after redirecting from another site
        /// Sample URL: http://localhost:5000/Home/CallBackResult?token=1&status=2&orderId=3&terminalNo=4&rrn=5
        /// </summary>
        [Authorize]
        public IActionResult CallBackResult(long token, string status, string orderId, string terminalNo, string rrn)
        {
            var userId = User.Identity.GetUserId();
            return Json(new { userId, token, status, orderId, terminalNo, rrn });
        }
    }
}