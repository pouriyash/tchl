using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Tochal.Core.DomainModels.SSOT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tochal.Web.Helpers
{
    public class HasRoleAttribute : ActionFilterAttribute
    {
        //private readonly AuthorityCode _authorityCode;

        //public HasRoleAttribute(AuthorityCode authorityCode)
        //{
        //    _authorityCode = authorityCode;
        //}

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (context.HttpContext.User.HasRole(_authorityCode))
        //    { base.OnActionExecuting(context); }
        //    else
        //    { context.Result = new JsonResult(ServiceResult.Error("you are not authorized")); }
        //    // context.Result = new ForbidResult();
            
        //    // context.Result = new UnauthorizedResult();
        //    // context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        //}
    }
}
