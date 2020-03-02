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

namespace Tochal.Web.Controllers.Banks
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("مدیریت فوتر")]
    public class FooterController : Controller
    {
        private readonly FooterService _FooterRepository;


        public FooterController(FooterService FooterService)
        {
            _FooterRepository = FooterService;
        }

        [DisplayName("مدیریت فوتر ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public IActionResult Change()
        {
            var model = _FooterRepository.GetInfo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Change(FooterInfo footerInfo,List<FooterColleagueViewModel> footerColleagues, IFormFile LogoImage,string LogoImageName)
        {
            var files = HttpContext.Request.Form.Files;

            if (LogoImage != null)
            {
                var LogoAddress = await FileUploadHelper.UploadFile(LogoImage);
                if (LogoAddress.Item1.Succeed)
                {
                    footerInfo.Logo = LogoAddress.Item2;
                }
            }
            if (LogoImageName!=null)
            {
                footerInfo.Logo = LogoImageName;
            }

            //var footerColleagueList = new List<FooterColleague>();

            //foreach (var item in footerColleagues)
            //{
            //    var footerColleagueAddress = await FileUploadHelper.UploadFile(item.ImageName);

            //    var entity = new FooterColleague();

            //    Mapper.Map(item, entity);

            //    if (footerColleagueAddress.Item1.Succeed)
            //    {
            //         entity.Image= footerColleagueAddress.Item2;
            //    }
            //    footerColleagueList.Add(entity);
            //}

            //آپلود عکس های همکاران
            var result = _FooterRepository.UpdateInfo(footerInfo);

            TempData.AddResult(result);

            return RedirectToAction(nameof(Change));
        }


    }
}