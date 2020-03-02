using System.ComponentModel;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tochal.Infrastructure.Services.Identity;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.Interface;
using Microsoft.AspNetCore.Http;
using Tochal.Infrastructure.Services.Routine2;
using Tochal.Infrastructure.Services;
using System;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Core.DomainModels.DTO.Routine2;
using System.Collections.Generic;

namespace Tochal.Web.Controllers
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("خانه")]
    [BreadCrumb(Title = "خانه", UseDefaultRouteUrl = true, Order = 0)]
    public class GeneralRoutine2Controller : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Routine2Repository _routine2Repository;
 
        public GeneralRoutine2Controller(IHttpContextAccessor contextAccessor, Routine2Repository routine2Repository)
        { 
            _contextAccessor = contextAccessor;
            _routine2Repository = routine2Repository;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="S"></typeparam>
       /// <typeparam name="T"></typeparam>
       /// <typeparam name="D"></typeparam>
       /// <param name="searchCriteria"></param>
       /// <param name="currentDashboard"></param>
        //public Routine2PageModel<List<T>, S, D> ManageMethod<S,T,D>(S searchCriteria, DashboardTypes currentDashboard,D routinedashboard) where S : Routine2SearchCriteria
        //    where T: IRoutine2DTO 
        //{
        //    var userId = _contextAccessor.HttpContext.User.Identity.GetUserId();

        //    // مراحل تازه‌های کارتابل فعلی چیست
        //    searchCriteria.RoutineStepList =
        //        _routine2Repository.GetRoleSteps(TestRoutine.RoutineId, currentDashboard.ToString());

        //    // کاربر در سمت فعلی، بر روی چه طرح‌هایی عملیاتی انجام داده است
        //    searchCriteria.RoutineLogList =
        //        _routine2Repository.GetUserEntityIds(TestRoutine.RoutineId, Convert.ToInt32(userId), currentDashboard.ToString());

        //    #region DashboardType: Draft, New, Archived, Done
        //    // پیش نویس‌ها
        //    if (searchCriteria.DashboardType == DashboardTypes.Draft)
        //        searchCriteria.RoutineIsFlown = false;

        //    if (searchCriteria.DashboardType == DashboardTypes.Done)
        //        searchCriteria.RoutineIsDone = true;
        //    #endregion

        //    var data = _testService.GetData(searchCriteria);

        //    var model = new Routine2PageModel<List<T>, S, D>(data, searchCriteria, routinedashboard);

        //    return View(model);
        //}
    }
}