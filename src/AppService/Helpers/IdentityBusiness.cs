using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Identity;

namespace Tochal.AppService.Helpers
{
    public static class IdentityBusiness // TODO : [SOREN] use Alamut.Utitlity.AspNet
    {
        public static ServiceResult AsServiceResult(this IdentityResult source)
        {
            return new ServiceResult
            {
                Succeed = source.Succeeded,
                Message = source.Succeeded
                    ? string.Empty
                    : source.Errors.Select(s => s.Description).Aggregate((a, b) => a + " " + b)
            };
        }

        public static ServiceResult<T> AsServiceResult<T>(this IdentityResult source, T data)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Succeed = source.Succeeded,
                Message = source.Succeeded
                    ? string.Empty
                    : source.Errors.Select(s => s.Description).Aggregate((a, b) => a + " " + b)
            };
        }
    }
}
