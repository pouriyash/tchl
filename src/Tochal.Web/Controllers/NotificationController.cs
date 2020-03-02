//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Tochal.Core.DomainModels.DTO; 
//using Tochal.Infrastructure.Services.Identity;
//using Tochal.Infrastructure.Services.Test;
//using Tochal.Web.Helpers; 
//using Research.City.Admin.Toolkit;
//using Tochal.Core.DomainModels.SSOT;
//using Alamut.Data.Structure;
//using Tochal.Core.DomainModels.ViewModel;
//using Microsoft.AspNetCore.Http;
//using Tochal.Core.Common.IdentityToolkit;

//namespace Tochal.Web.Controllers.Banks
//{
//    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
//    [DisplayName("بانک پیام")]

//    public class NotificationController :Controller
//    {
//        private readonly NotificationUserService _NotificationUserService;
//        private readonly IHttpContextAccessor _contextAccessor;

//        public NotificationController(NotificationUserService NotificationUserService, IHttpContextAccessor contextAccessor)
//        {
//            _contextAccessor = contextAccessor;
//            _NotificationUserService = NotificationUserService;
//        }
  
//        [HttpPost]
//        [DisplayName("ویرایش")]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(NotificationViewModel vm)
//        {
//            var result = _NotificationUserService.Create(vm);
//            TempData.AddResult(result);

//            return RedirectToAction("Index");
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var model =await _NotificationUserService.GetByCondition<NotificationSummaryDTO>(p=>p.Id==id);
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(NotificationViewModel vm,int Id)
//        { 
//            var result =await _NotificationUserService.Edit(Id, vm);
//            TempData.AddResult(result);
//            return RedirectToAction("Index");
//        }

//        [DisplayName("حذف")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var data = _NotificationUserService.GetByCondition<NotificationSummaryDTO>(p => p.Id == id);

//            try
//            {
//                var model = await _NotificationUserService.Delete(id);
//                TempData.AddResult(model);
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                TempData.AddResult(ServiceResult.Error("از این فیلد در سامانه استفاده شده است."));
//                return RedirectToAction("Index");
//            }
//        }

      

//        public bool IsRead(int id)
//        {
//             var status = _NotificationUserService.IsRead(id);
//            return status;
//        }
//    }
//}