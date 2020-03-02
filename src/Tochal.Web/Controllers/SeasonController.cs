using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Infrastructure.Services.Test;
using Tochal.Web.Helpers;
using Research.City.Admin.Toolkit;
using Tochal.Infrastructure.Services.Identity;
using System.ComponentModel;
using Alamut.Data.Structure;
using Tochal.Infrastructure.Services;
using DNTCommon.Web.Core;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochal.Core.DomainModels.Entity;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Core.DomainModels.DTO;

namespace Tochal.Web.Controllers.Banks
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("مدیریت فصل ها")]
    public class SeasonController : Controller
    {
        private readonly SeasonService _SeasonService;

        public SeasonController(SeasonService SeasonService)
        {
            _SeasonService = SeasonService;
        }
        [DisplayName("مدیریت فصل ها")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult Index()
        {
            return View();
        }

        [DisplayName("فصل پاییز ")]
        public IActionResult Winter()
        {
            var model = _SeasonService.Get();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWinter(Season season, IFormFile WinterHeaderImage, IFormFile WinterHeaderMobileImage
             , string WinterHeaderImageName, string WinterHeaderMobileImageName)
        {
            if (WinterHeaderImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(WinterHeaderImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    season.WinterHeaderImage = headerImageAddress.Item2;
                }
            }
            else if (WinterHeaderImageName != null)
            {
                season.WinterHeaderImage = WinterHeaderImageName;
            }

            if (WinterHeaderMobileImage != null)
            {
                var headerMobileImageAddress = await FileUploadHelper.UploadFile(WinterHeaderMobileImage);
                if (headerMobileImageAddress.Item1.Succeed)
                {
                    season.WinterHeaderMobileImage = headerMobileImageAddress.Item2;
                }
            }
            else if (WinterHeaderMobileImageName != null)
            {
                season.WinterHeaderMobileImage = WinterHeaderMobileImageName;
            }

            var result=_SeasonService.UpdateWinter(season);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Winter));
        }

        [DisplayName("فصل زمستان ")]
        public IActionResult Autumn()
        {
            var model = _SeasonService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAutumn(Season season, IFormFile AutumnHeaderImage, IFormFile AutumnHeaderMobileImage
             , string AutumnHeaderImageName, string AutumnHeaderMobileImageName)
        {
            if (AutumnHeaderImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(AutumnHeaderImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    season.AutumnHeaderImage = headerImageAddress.Item2;
                }
            }
            else if (AutumnHeaderImageName != null)
            {
                season.AutumnHeaderImage = AutumnHeaderImageName;
            }

            if (AutumnHeaderMobileImage != null)
            {
                var headerMobileImageAddress = await FileUploadHelper.UploadFile(AutumnHeaderMobileImage);
                if (headerMobileImageAddress.Item1.Succeed)
                {
                    season.AutumnHeaderMobileImage = headerMobileImageAddress.Item2;
                }
            }
            else if (AutumnHeaderMobileImageName != null)
            {
                season.AutumnHeaderMobileImage = AutumnHeaderMobileImageName;
            }

            var result = _SeasonService.UpdateAutumn(season);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Autumn));
        }

        [DisplayName("فصل بهار ")]
        public IActionResult Spring()
        {
            var model = _SeasonService.Get();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpring(Season season, IFormFile SpringHeaderImage, IFormFile SpringHeaderMobileImage
            ,string SpringHeaderImageName,string SpringHeaderMobileImageName)
        {

            if (SpringHeaderImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(SpringHeaderImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    season.SpringHeaderImage = headerImageAddress.Item2;
                }
            }
            else if (SpringHeaderImageName != null)
            {
                season.SpringHeaderImage = SpringHeaderImageName;
            }
             

            if (SpringHeaderMobileImage != null)
            {
                var headerMobileImageAddress = await FileUploadHelper.UploadFile(SpringHeaderMobileImage);
                if (headerMobileImageAddress.Item1.Succeed)
                {
                    season.SpringHeaderMobileImage = headerMobileImageAddress.Item2;
                }
            }
            else if (SpringHeaderMobileImageName != null)
            {
                season.SpringHeaderMobileImage = SpringHeaderMobileImageName;
            }

            var result = _SeasonService.UpdateSpring(season);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Spring));
        }

        [DisplayName("فصل تابستان ")]
        public IActionResult Summer()
        {
            var model = _SeasonService.Get();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSummer(Season season, IFormFile SummerHeaderImage, IFormFile SummerHeaderMobileImage
            , string SummerHeaderImageName, string SummerHeaderMobileImageName)
        {
            if (SummerHeaderImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(SummerHeaderImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    season.SummerHeaderImage = headerImageAddress.Item2;
                }
            }
            else if (SummerHeaderImageName != null)
            {
                season.SummerHeaderImage = SummerHeaderImageName;
            }



            if (SummerHeaderMobileImage != null)
            {
                var headerMobileImageAddress = await FileUploadHelper.UploadFile(SummerHeaderMobileImage);
                if (headerMobileImageAddress.Item1.Succeed)
                {
                    season.SummerHeaderMobileImage = headerMobileImageAddress.Item2;
                }
            }
            else if (SummerHeaderMobileImageName != null)
            {
                season.SummerHeaderMobileImage = SummerHeaderMobileImageName;
            }

            var result = _SeasonService.UpdateSummer(season);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Summer));
        }

    }

}