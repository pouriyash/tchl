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
    public class GroupGalleryController : Controller
    {
        private readonly GroupGalleryService _GroupGalleryService;
        private readonly GalleryEntityService _GalleryEntityService;
        private readonly GalleryService _GalleryService;

        public GroupGalleryController(GroupGalleryService GroupGalleryService, GalleryService GalleryService, GalleryEntityService GalleryEntityService)
        {
            _GroupGalleryService = GroupGalleryService;
            _GalleryService = GalleryService;
            _GalleryEntityService = GalleryEntityService;
        }

        [DisplayName("مدیریت گالری تصاویر ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public async Task<IActionResult> Index(GroupGallerySearchViewModel search)
        {
            var conditions = new ConditionHelper<GroupGalleryDTO>();

            conditions.AddCondition(p => p.GalleryType == GalleryTypeSSOT.Image && p.GalleryForType == GalleryForTypeSSOT.Gallery);

            if (search.Title != null)
                conditions.AddCondition(p => p.Title.Contains(search.Title));

            var data = await _GroupGalleryService.GetAll(conditions.GetConditionList());

            var TotalCount = await _GroupGalleryService.TotalCount();

            var model = new SearchCriteriaPageModel<List<GroupGalleryDTO>, GroupGallerySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }
         
        [DisplayName("مدیریت گالری تصاویر محتوا ")]
        [GroupingDashboard(GroupingDashboardSSOT.Content)]
        public async Task<IActionResult> IndexGalleries(GroupGallerySearchViewModel search)
        {
            var conditions = new ConditionHelper<GroupGalleryDTO>();

            conditions.AddCondition(p => p.GalleryType == GalleryTypeSSOT.Image&& p.GalleryForType == GalleryForTypeSSOT.Content);

            if (search.Title != null)
                conditions.AddCondition(p => p.Title.Contains(search.Title));

            var data = await _GroupGalleryService.GetAll(conditions.GetConditionList());

            var TotalCount = await _GroupGalleryService.TotalCount();

            var model = new SearchCriteriaPageModel<List<GroupGalleryDTO>, GroupGallerySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        [DisplayName("مدیریت گالری کلیپ ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public async Task<IActionResult> Clips(GroupGallerySearchViewModel search)
        {
            var conditions = new ConditionHelper<GroupGalleryDTO>();

            conditions.AddCondition(p => p.GalleryType == GalleryTypeSSOT.Clip && p.GalleryForType == GalleryForTypeSSOT.Gallery);

            if (search.Title != null)
                conditions.AddCondition(p => p.Title.Contains(search.Title));

            var data = await _GroupGalleryService.GetAll(conditions.GetConditionList());

            var TotalCount = await _GroupGalleryService.TotalCount();

            var model = new SearchCriteriaPageModel<List<GroupGalleryDTO>, GroupGallerySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        //public async Task<IActionResult> Create(GalleryTypeSSOT GalleryType)
        //{
        //    ViewBag.GalleryType = GalleryType;
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(GroupGalleryViewModel vm,IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(ImageFile);
                if (headerImageAddress.Item1.Succeed)
                {
                    vm.Image = headerImageAddress.Item2;
                }
            }

            var result = _GroupGalleryService.Create(vm);
            TempData.AddResult(result);

            if (vm.GalleryForType == GalleryForTypeSSOT.Gallery)
            {
                if (vm.GalleryType == GalleryTypeSSOT.Image)
                    return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Clips));
            }
            else
            {
                return RedirectToAction(nameof(IndexGalleries));

            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _GroupGalleryService.Find(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupGalleryViewModel vm, int id,IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(ImageFile);
                if (headerImageAddress.Item1.Succeed)
                {
                    vm.Image = headerImageAddress.Item2;
                }
            }

            var model =await _GroupGalleryService.Find(id);
            var result = await _GroupGalleryService.Edit(id, vm);
            TempData.AddResult(result);

            if (model.GalleryForType == GalleryForTypeSSOT.Gallery)
            {
                if (model.GalleryType == GalleryTypeSSOT.Image)
                    return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Clips));
            }
            else
            {
                return RedirectToAction(nameof(IndexGalleries));

            }
        }

        public async Task<IActionResult> Delete(int id, GalleryTypeSSOT GalleryType, GalleryForTypeSSOT GalleryForType)
        {
            try
            {

                if (GalleryForType==GalleryForTypeSSOT.Content)
                {
                    _GalleryEntityService.RemoveByGroupGalleryId(GalleryType, GalleryContentTypeSSOT.Content, id);

                }
                else
                {
                    _GalleryEntityService.RemoveByGroupGalleryId(GalleryType, GalleryContentTypeSSOT.Menu, id);

                }

                //حذف عکس پیشفرض
                _GalleryService.RemoveFirstImage(id);
            
                //حذف تمام عکس هاش
                _GalleryService.RemoveAllByGroupGalleryId(id);
                
                var result = await _GroupGalleryService.Delete(id);
                TempData.AddResult(result);

                if (GalleryForType == GalleryForTypeSSOT.Gallery)
                {
                    if (GalleryType == GalleryTypeSSOT.Image)
                        return RedirectToAction(nameof(Index));
                    return RedirectToAction(nameof(Clips));
                }
                else
                {
                    return RedirectToAction(nameof(IndexGalleries));

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}