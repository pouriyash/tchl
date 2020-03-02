using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tochal.Core.DomainModels.DTO;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Infrastructure.Services.Test;
using Tochal.Web.Helpers;
using Research.City.Admin.Toolkit;
using Tochal.Infrastructure.Services.Identity;
using System.ComponentModel;
using Alamut.Data.Structure;
using Tochal.Infrastructure.Services;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using DNTCommon.Web.Core;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochal.Core.DomainModels.Entity.Menu;
using Microsoft.AspNetCore.Http;
using Tochal.Core.DomainModels.Entity.Content;

namespace Tochal.Web.Controllers.Banks
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("مدیریت منو ها")]
    public class MenuController : Controller
    {
        private readonly MenuService _MenuRepository;
        private readonly GroupGalleryService _GroupGalleryService;
        private readonly GalleryEntityService _GalleryEntityService;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;


        public MenuController(MenuService MenuService, GroupGalleryService GroupGalleryService, GalleryEntityService GalleryEntityService, IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            _GroupGalleryService = GroupGalleryService;
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _MenuRepository = MenuService;
            _GalleryEntityService = GalleryEntityService;
        }

        [DisplayName("مدیریت منو ها ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public async Task<IActionResult> Index(MenuEntitySearchViewModel search)
        {
            var conditions = new ConditionHelper<MenuEntityDTO>();

            conditions.AddCondition(p => p.Row == search.RowType);

            conditions.AddCondition(p => p.ParentId == search.ParentId);

            if (search.Title != null)
                conditions.AddCondition(p => p.Title.Contains(search.Title));

            var data = await _MenuRepository.GetAll(conditions.GetConditionList());

            var TotalCount = await _MenuRepository.TotalCount();

            var model = new SearchCriteriaPageModel<List<MenuEntityDTO>, MenuEntitySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        public Dictionary<string, string> GetAllContentLink(params Assembly[] assemblies)
        {
            var type = typeof(MenuFilterAttribute);
            var model = assemblies.SelectMany(p => p.GetTypes()).Where(p => p.HasAttribute(type));

            var actionAddressList = new Dictionary<string, string>();

            foreach (var item in model)
            {
                var list = item.GetMethods().Where(p => p.HasAttribute(type));
                foreach (var item2 in list)
                {
                    var t = item2.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;

                    actionAddressList.Add(item2.DeclaringType.Name.Replace("Controller", "") + "/" + item2.Name, t.DisplayName);
                }
            }

            return actionAddressList;
        }

        public IActionResult create(int? ParentId, RowSSOT rowType = RowSSOT.First)
        {
            if (rowType == RowSSOT.First)
            {
                var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image); 

                ViewBag.GalleryList = GalleryList;
                return View(viewName: "~/Views/Menu/createParent.cshtml");
            }
            else if (rowType == RowSSOT.Title)
            {
                ViewBag.ParentId = ParentId;
                ViewBag.rowType = rowType;

                return View(viewName: "~/Views/Menu/createTitle.cshtml");
            }
            else
            {
                ViewBag.ParentId = ParentId;
                ViewBag.rowType = rowType;
                var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image);
                ViewBag.GalleryList = GalleryList;
                return View(viewName: "~/Views/Menu/createChild.cshtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ایجاد ")]
        public async Task<IActionResult> create(MenuEntityViewModel vm
            , IFormFile headerImage
            , IFormFile MobileImage
            , IFormFile mainVideo
            , List<int> Gallery)
        {

            if (headerImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(headerImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    vm.HeaderImage = headerImageAddress.Item2;
                }
            }
            if (MobileImage != null)
            {
                var MobileImageAddress = await FileUploadHelper.UploadFile(MobileImage);
                if (MobileImageAddress.Item1.Succeed)
                {
                    vm.MobileImage = MobileImageAddress.Item2;
                }
            }
            if (mainVideo != null)
            {
                var mainVideoAddress = await FileUploadHelper.UploadFile(mainVideo);
                if (mainVideoAddress.Item1.Succeed)
                {
                    vm.video = mainVideoAddress.Item2;
                }
            }
            var result = _MenuRepository.Add(vm);

            //if (result.Succeed && galleryImage.Count > 0)
            //{
            //    var GalleryList = await FileUploadHelper.UploadFile(galleryImage);
            //    _GalleryService.Create(GalleryList.Item2, result.Data, Core.DomainModels.Entity.Content.GalleryTypeSSOT.Menu);

            //}

            if (Gallery.Count > 0)
            {
                foreach (var item in Gallery)
                {
                    _GalleryEntityService.Create(new GalleryEntityViewModel
                    {
                        EntityId = result.Data,
                        galleryGroupId = item,
                        GalleryContentType = GalleryContentTypeSSOT.Menu,
                        GalleryType = GalleryTypeSSOT.Image
                    });
                }
            }
            TempData.AddResult(result);

            return RedirectToAction("Index", new { RowType = vm.Row, ParentId = vm.ParentId });
        }

        [DisplayName("ویرایش ")]
        public async Task<IActionResult> Edit(int id, RowSSOT Row)
        {

            if (Row == RowSSOT.First)
            {
                var model = await _MenuRepository.GetByCondition<MenuEntityParentDTO>(p => p.Id == id);

                var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image);
                var GalleryEntityList = _GalleryEntityService.List(GalleryTypeSSOT.Image, GalleryContentTypeSSOT.Menu, id);
                return View(viewName: "~/Views/Menu/EditParent.cshtml", new Tuple<MenuEntityParentDTO,
                     List<GalleryEntity>, List<GroupGallery>>(model, GalleryEntityList, GalleryList));
            }
            else if (Row == RowSSOT.Title)
            {
                var model = await _MenuRepository.GetByCondition<MenuEntityTitleDTO>(p => p.Id == id);

                return View(viewName: "~/Views/Menu/EditTitle.cshtml", model);
            }
            else
            {
                var model = await _MenuRepository.GetByCondition<MenuEntityChildDTO>(p => p.Id == id);

                var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image);
                var GalleryEntityList = _GalleryEntityService.List(GalleryTypeSSOT.Image, GalleryContentTypeSSOT.Menu, id);
                return View(viewName: "~/Views/Menu/EditChild.cshtml", new Tuple<MenuEntityChildDTO,
                    List<GalleryEntity>, List<GroupGallery>>(model, GalleryEntityList, GalleryList));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuEntityEditViewModel vm,
            IFormFile headerImage, string headerImageName,
            IFormFile mobileImage, string mobileImageName,
            IFormFile mainVideo, string videoName
            , List<int> Gallery)
        {
            if (headerImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(headerImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    vm.HeaderImage = headerImageAddress.Item2;
                }
            }
            else if (headerImageName != null)
            {
                vm.HeaderImage = headerImageName;
            }

            if (mainVideo != null)
            {
                var mainVideoAddress = await FileUploadHelper.UploadFile(mainVideo);
                if (mainVideoAddress.Item1.Succeed)
                {
                    vm.Video = mainVideoAddress.Item2;
                }
            }
            else if (videoName != null)
            {
                vm.Video = videoName;
            }

            if (mobileImage != null)
            {
                var mobileImageAddress = await FileUploadHelper.UploadFile(mobileImage);
                if (mobileImageAddress.Item1.Succeed)
                {
                    vm.MobileImage = mobileImageAddress.Item2;
                }
            }
            else if (mobileImageName != null)
            {
                vm.MobileImage = mobileImageName;
            }

            //_GalleryEntityService.RemoveAll(GalleryTypeSSOT.Menu, vm.Id);
             
            if (Gallery.Count > 0)
            {
                _GalleryEntityService.RemoveAll(GalleryTypeSSOT.Image,GalleryContentTypeSSOT.Menu, vm.Id);
                foreach (var item in Gallery)
                {
                    _GalleryEntityService.Create(new GalleryEntityViewModel
                    {
                        EntityId = vm.Id,
                        galleryGroupId = item,
                        GalleryContentType = GalleryContentTypeSSOT.Menu,
                        GalleryType = GalleryTypeSSOT.Image
                    });
                }
            }

            var result = await _MenuRepository.Edit(vm.Id, vm);
            TempData.AddResult(result);

            return RedirectToAction("Index", new { RowType = vm.Row, ParentId = vm.ParentId });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ApiGetAll()
        {
            var model = _MenuRepository.ApiGetAll();
            return Json(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ApiGetChilds(int parentId)
        {
            var model = _MenuRepository.ApiGetChilds(parentId);
            return Json(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ApiGetParents()
        {
            var model = _MenuRepository.ApiGetParents();
            return Json(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ApiGetTitles(int parentId)
        {
            var model = _MenuRepository.ApiGetTitles(parentId);
            return Json(model);
        }
        public async Task<IActionResult> DeleteParent(int Id)
        {
            try
            {
                var result = await _MenuRepository.Delete(Id);
                TempData.AddResult(result);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> DeleteTitle(int Id, int parentId)
        {
            try
            {
                var result = await _MenuRepository.Delete(Id);
                TempData.AddResult(result);

                return RedirectToAction(nameof(Index), new { ParentId = parentId, RowType = RowSSOT.Title });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> DeleteChild(int Id, int parentId)
        {
            try
            {
                var result = await _MenuRepository.Delete(Id);
                TempData.AddResult(result);

                return RedirectToAction(nameof(Index), new { ParentId = parentId, RowType = RowSSOT.Second });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}