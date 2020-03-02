//using Tochal.Core.Common.IdentityToolkit;
//using Tochal.Core.DomainModels.DTO;
//using Tochal.Infrastructure.Services.Test;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Tochal.Web
//{
//    public class Notification : ViewComponent
//    {
//        private readonly NotificationUserService _NotificationUserService;
//        private readonly IHttpContextAccessor _contextAccessor;

//        public Notification(NotificationUserService NotificationUserService, IHttpContextAccessor contextAccessor)
//        { _contextAccessor = contextAccessor;
//            _NotificationUserService = NotificationUserService;
//        }

//      public IViewComponentResult Invoke()
//        {
//            var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.Identity.GetUserId());
//            var list = _NotificationUserService.GetAllNotificationforUser(userId);
//            return View(viewName: "Default", model: list);
//         }
//    }
//}
