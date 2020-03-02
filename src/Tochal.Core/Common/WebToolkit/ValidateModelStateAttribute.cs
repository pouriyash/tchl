using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.Common.WebToolkit
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// قبل از آنکه کد اکشن مورد نظر اجرا شود این قسمت
        /// اجرا شده و درصورتی که خطایی وجود داشته باشد 
        /// گزارش میکند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }

    }
}
