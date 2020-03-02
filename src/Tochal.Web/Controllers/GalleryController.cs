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
    [DisplayName("مدیریت گالری")]
    public class GalleryController : Controller
    {
        private readonly GalleryService _GalleryService;
        private readonly GroupGalleryService _GroupGalleryService;

        public GalleryController(GalleryService GalleryService, GroupGalleryService GroupGalleryService)
        {
            _GroupGalleryService = GroupGalleryService;
            _GalleryService = GalleryService;
        }

        [DisplayName("مدیریت گالری ")]
        public async Task<IActionResult> Index(GallerySearchViewModel search)
        {
            if (search.GroupGalleryId == 0)
            {
                TempData.AddResult(ServiceResult.Error("آدرس وارد شده اشتباه است!"));
                return RedirectToAction("Index", "Home");
            }
            var conditions = new ConditionHelper<GalleryDTO>();
            conditions.AddCondition(p => p.GroupGalleryId == search.GroupGalleryId);
            var data = await _GalleryService.GetAll(conditions.GetConditionList());

            var TotalCount = await _GalleryService.TotalCount();

            var model = new SearchCriteriaPageModel<List<GalleryDTO>, GallerySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        [DisplayName("مدیریت گالری ")]
        public async Task<IActionResult> clips(GallerySearchViewModel search)
        {
            if (search.GroupGalleryId == 0)
            {
                TempData.AddResult(ServiceResult.Error("آدرس وارد شده اشتباه است!"));
                return RedirectToAction("Index", "Home");
            }
            var conditions = new ConditionHelper<GalleryDTO>();
            conditions.AddCondition(p => p.GroupGalleryId == search.GroupGalleryId);
            var data = await _GalleryService.GetAll(conditions.GetConditionList());

            var TotalCount = await _GalleryService.TotalCount();

            var model = new SearchCriteriaPageModel<List<GalleryDTO>, GallerySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        public IActionResult Create(int GroupGalleryId)
        {
            ViewBag.GroupGalleryId = GroupGalleryId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile Image, string Alt, int GroupGalleryId, GalleryTypeSSOT GalleryType)
        {
            var vm = new GalleryViewModel() { Alt = Alt, GroupGalleryId = GroupGalleryId };
            if (Image != null)
            {
                var ImageAddress = await FileUploadHelper.UploadFile(Image);
                if (ImageAddress.Item1.Succeed)
                {
                    vm.Image = ImageAddress.Item2;
                }
            }
            var result = _GalleryService.Create(vm);
            TempData.AddResult(result);

            if (GalleryType == GalleryTypeSSOT.Clip)
                return RedirectToAction(nameof(clips), new { GroupGalleryId });
            return RedirectToAction(nameof(Index), new { GroupGalleryId });

        }

        public async Task<IActionResult> Delete(int id, int GroupGalleryId, GalleryTypeSSOT GalleryType)
        {
            //حذف عکس 
            var result = await _GalleryService.Delete(id);
            TempData.AddResult(result);

            if (GalleryType == GalleryTypeSSOT.Clip)
                return RedirectToAction(nameof(clips), new { GroupGalleryId });
            return RedirectToAction(nameof(Index), new { GroupGalleryId });
        }

        public IActionResult SetFirstImage(int id, int GroupGalleryId)
        {
            _GalleryService.SetFirstImage(id, GroupGalleryId);
            TempData.AddResult(ServiceResult.Okay());
            return RedirectToAction(nameof(Index), new { GroupGalleryId });
        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await _GalleryService.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, string Alt, int GroupGalleryId, GalleryTypeSSOT GalleryType)
        {
            _GalleryService.EditText(id, Alt);
            TempData.AddResult(ServiceResult.Okay());
            if (GalleryType == GalleryTypeSSOT.Clip)
                return RedirectToAction(nameof(clips), new { GroupGalleryId });
            return RedirectToAction(nameof(Index), new { GroupGalleryId });
        }
    }

}