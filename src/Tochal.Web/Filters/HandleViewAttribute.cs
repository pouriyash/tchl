using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Tochal.Core.DomainModels.SSOT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tochal.Infrastructure.Services;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Tochal.Web.Filters
{
    public class HandleViewAttribute: ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var contentManagementService = filterContext.HttpContext.RequestServices.GetService<ContentManagementService>();
            var actiondescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            var viewDetailType = contentManagementService.GetTypeOfViewDetail(actiondescriptor.ControllerName+"/"+ actiondescriptor.ActionName);
            if (viewDetailType!=null)
                filterContext.RouteData.Values.Add("viewDetailType", ((int)viewDetailType).ToString());
        }
    }
}
