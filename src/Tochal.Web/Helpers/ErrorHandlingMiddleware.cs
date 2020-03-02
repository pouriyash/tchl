using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace Tochal.Web.Helpers
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseServiceResultErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ServiceResultErrorHandlingMiddleware>();
        }
    }
}
