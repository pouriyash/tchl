using System;
using System.Security.Claims;
using Tochal.Core.DomainModels.SSOT;
using Microsoft.AspNetCore.Http;


namespace Tochal.Web.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        //private static readonly string SystemSupervisor = AuthorityCode.SystemSupervisor.ToString();

        //public static bool HasRole(this ClaimsPrincipal principal, AuthorityCode code)
        //{
        //    if (principal.IsInRole(SystemSupervisor)) return true;
        //    if (principal.IsInRole(code.ToString())) return true;
        //    if (AuthorityCodeBusiness.HasParentOf(code, principal.IsInRole)) return true;

        //    try
        //    {
        //        var fullAccessCode = ((int)code / 1000) * 1000;
        //        AuthorityCode fullAccessCodeEnum = (AuthorityCode)fullAccessCode;
        //        var hasFullAccessCode = principal.IsInRole(fullAccessCodeEnum.ToString());
        //        return hasFullAccessCode;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public static string GetNationalCode(this ClaimsPrincipal principal) =>
        //    principal.FindFirstValue(ClaimTypes.Sid);

        //public static bool IsSystemSupervisor(this ClaimsPrincipal principal) => principal.IsInRole(SystemSupervisor);

        

    }

}
