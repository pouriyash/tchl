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
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.DTO.ContentEntity;
using Tochal.Core.DomainModels.Entity.Menu;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using Tochal.Core.DomainModels.Entity.Content;

namespace Tochal.Web.Controllers.Banks
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("بانک مدیریت محتوا")]
    public class ContentManagementController : Controller
    {
        private readonly ContentManagementService _ContentManagementRepository;
        private readonly GalleryService _GalleryService;
        private readonly GalleryEntityService _GalleryEntityService;
        private readonly GroupGalleryService _GroupGalleryService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ContentManagementController(ContentManagementService ContentManagementService, GalleryEntityService GalleryEntityService, GroupGalleryService GroupGalleryService, GalleryService GalleryService, IHostingEnvironment hostingEnvironment)
        {
            _GalleryEntityService = GalleryEntityService;
            _GroupGalleryService = GroupGalleryService;
            _GalleryService = GalleryService;
            _ContentManagementRepository = ContentManagementService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(ContentEntitySearchViewModel search, MainPageContentTypeSSOT? MainPageContentType, ShowlocationTypeSSOT Showlocation)
        {
            var conditions = new ConditionHelper<ContentEntityDTO>();

            search.MainPageContentType = MainPageContentType;
            search.Showlocation = Showlocation;

            conditions.AddCondition(p => p.Showlocation.Equals(search.Showlocation));

            if (MainPageContentType != null)
                conditions.AddCondition(p => p.MainPageContentType.Equals(MainPageContentType));

            if (search.Title != null)
                conditions.AddCondition(p => p.Title.Contains(search.Title));

            var data = await _ContentManagementRepository.GetAll(conditions.GetConditionList());

            var TotalCount = data.Count();

            var model = new SearchCriteriaPageModel<List<ContentEntityDTO>, ContentEntitySearchViewModel, int>(data, search, TotalCount);
            return View(model);
        }

        public IActionResult Create(ShowlocationTypeSSOT Showlocation, MainPageContentTypeSSOT? MainPageContentType)
        {
            ViewBag.Showlocation = Showlocation;

            if (Showlocation == ShowlocationTypeSSOT.FirstPage)
            {
                ViewBag.MainPageContentType = MainPageContentType;
            }
            var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image);
            ViewBag.GalleryList = GalleryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ایجاد ")]
        public async Task<IActionResult> Create(ContentEntityViewModel vm, IFormFile mainVideo
            , IFormFile mainImage, List<int> Gallery, IFormFile headerImage, IFormFile MobileImage)
        {
            //بررسی فایل ها 

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
                    vm.MainVideo = mainVideoAddress.Item2;
                }
            }

            if (mainImage != null)
            {
                var mainImageAddress = await FileUploadHelper.UploadFile(mainImage);
                if (mainImageAddress.Item1.Succeed)
                {
                    vm.MainImage = mainImageAddress.Item2;
                }
            }

            vm.CreatedDate = DateTime.Now;
            var result = _ContentManagementRepository.Add(vm);

            //if (result.Succeed && galleryImage.Count > 0)
            //{
            //    var GalleryList = await FileUploadHelper.UploadFile(galleryImage);
            //    _GalleryService.Create(GalleryList.Item2, result.Data, Core.DomainModels.Entity.Content.GalleryTypeSSOT.Content);

            //}

            if (Gallery.Count > 0)
            {
                foreach (var item in Gallery)
                {
                    _GalleryEntityService.Create(new GalleryEntityViewModel
                    {
                        EntityId = result.Data,
                        galleryGroupId = item,
                        GalleryContentType = GalleryContentTypeSSOT.Content,
                        GalleryType = GalleryTypeSSOT.Image
                    });
                }
            }
            TempData.AddResult(result);

            return RedirectToAction(nameof(Index), new { vm.Showlocation, vm.MainPageContentType });
        }


        [DisplayName("ویرایش ")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _ContentManagementRepository.GetByCondition<ContentEntityDTO>(p => p.Id == id);
            var GalleryList = _GroupGalleryService.List(GalleryForTypeSSOT.Content, GalleryTypeSSOT.Image);
            var GalleryEntityList = _GalleryEntityService.List(GalleryTypeSSOT.Image, GalleryContentTypeSSOT.Content, id);

            return View(new Tuple<ContentEntityDTO, List<GalleryEntity>, List<GroupGallery>>(model, GalleryEntityList, GalleryList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContentEntityViewModel vm, int Id, IFormFile mainVideo, string mainVideoName
            , IFormFile mainImage, List<int> Gallery
            , string mainImageName,
            IFormFile headerImage, string headerImageName
            , IFormFile MobileImage, string mobileImageName)
        {
            //بررسی فایل ها 

            if (mainVideoName != null)
            {
                vm.MainVideo = mainVideoName;
            }
            if (mainVideo != null)
            {
                var mainVideoAddress = await FileUploadHelper.UploadFile(mainVideo);
                if (mainVideoAddress.Item1.Succeed)
                {
                    vm.MainVideo = mainVideoAddress.Item2;
                }
            }

            if (mobileImageName != null)
            {
                vm.MobileImage = mobileImageName;
            }
            if (MobileImage != null)
            {
                var MobileImageAddress = await FileUploadHelper.UploadFile(MobileImage);
                if (MobileImageAddress.Item1.Succeed)
                {
                    vm.MobileImage = MobileImageAddress.Item2;
                }
            }
            if (headerImageName != null)
            {
                vm.HeaderImage = headerImageName;
            }

            if (headerImage != null)
            {
                var headerImageAddress = await FileUploadHelper.UploadFile(headerImage);
                if (headerImageAddress.Item1.Succeed)
                {
                    vm.HeaderImage = headerImageAddress.Item2;
                }
            }

            if (mainImageName != null)
            {
                vm.MainImage = mainImageName;
            }

            if (mainImage != null)
            {
                var mainImageAddress = await FileUploadHelper.UploadFile(mainImage);
                if (mainImageAddress.Item1.Succeed)
                {
                    vm.MainImage = mainImageAddress.Item2;
                }
            }



            _GalleryEntityService.RemoveAll(GalleryTypeSSOT.Image, GalleryContentTypeSSOT.Content, Id);
            if (Gallery.Count > 0)
            {
                foreach (var item in Gallery)
                {
                    _GalleryEntityService.Create(new GalleryEntityViewModel
                    {
                        EntityId = Id,
                        galleryGroupId = item,
                        GalleryContentType = GalleryContentTypeSSOT.Content,
                        GalleryType = GalleryTypeSSOT.Image
                    });
                }
            }

            var result = await _ContentManagementRepository.Edit(Id, vm);
            TempData.AddResult(result);

            return RedirectToAction(nameof(Index), new { vm.Showlocation, vm.MainPageContentType });
        }

        public async Task<IActionResult> Delete(int Id, ShowlocationTypeSSOT Showlocation, MainPageContentTypeSSOT? MainPageContentType)
        {
            var result = await _ContentManagementRepository.Delete(Id);
            TempData.AddResult(result);

            return RedirectToAction(nameof(Index), new { Showlocation, MainPageContentType });
        }

        [MenuFilter]
        [DisplayName("لیست ویدیو ها")]
        public async Task<IActionResult> GetAllVideoList(DetailTypeSSOT detailType)
        {
            TempData["PageType"] = detailType;
            var conditions = new ConditionHelper<ContentEntityDTO>();

            conditions.AddCondition(p => p.BlogContentEntityType == BlogContentEntityTypeSSOT.Video);

            var list = await _ContentManagementRepository.GetAll(conditions.GetConditionList());

            return View("MainView", list);
        }

        [MenuFilter]
        [DisplayName("لیست تصاویر")]
        public async Task<IActionResult> GetAllImageList(DetailTypeSSOT detailType)
        {
            TempData["PageType"] = detailType;
            var conditions = new ConditionHelper<ContentEntityDTO>();

            conditions.AddCondition(p => p.BlogContentEntityType == BlogContentEntityTypeSSOT.Image);

            var list = await _ContentManagementRepository.GetAll(conditions.GetConditionList());

            return View("MainView", list);
        }

        [MenuFilter]
        [DisplayName("لیست متون")]
        public async Task<IActionResult> GetAllTextList(DetailTypeSSOT detailType)
        {
            TempData["PageType"] = detailType;

            var conditions = new ConditionHelper<ContentEntityDTO>();

            conditions.AddCondition(p => p.BlogContentEntityType == BlogContentEntityTypeSSOT.Text);

            var list = await _ContentManagementRepository.GetAll(conditions.GetConditionList());

            return View("MainView", list);
        }


        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            // Get the file from the POST request
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "repository", "uploads");

            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            // Basic validation on mime types and file extension
            string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

            try
            {
                if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable();
                    imageUrl.Add("link", "http://185.10.74.119:1200/repository/uploads/" + name);

                    return Json(imageUrl);
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public async Task<bool> DeleteGallery(int id)
        {
            var result = await _GalleryService.Delete(id);
            //حذف عکس از فولدر
            if (result.Succeed)
                return true;
            return false;
        }

        [MenuFilter]
        [DisplayName("مدیریت اسلایدر")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult SliderContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.Slider });
        }

        [MenuFilter]
        [DisplayName("مدیریت رویدادها")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult EventContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.Event });
        }

        [MenuFilter]
        [DisplayName("مدیریت پر بازدیدترن ها")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult MostVistContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.MostVist });
        }

        [MenuFilter]
        [DisplayName("مدیریت اخبار")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult NewsContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.News });
        }

        //[MenuFilter]
        //[DisplayName("مدیریت فصل ها")]
        //[GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        //public IActionResult SeasonContent(ContentEntitySearchViewModel search)
        //{
        //    return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.Season });
        //}

        [MenuFilter]
        [DisplayName("مدیریت رویداد های ویژه")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult SpecialEventContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.SpecialEvent });
        }

        [MenuFilter]
        [DisplayName("مدیریت بلیط ها")]
        [GroupingDashboard(GroupingDashboardSSOT.FirstPage)]
        public IActionResult TicketContent(ContentEntitySearchViewModel search)
        {
            return RedirectToAction(nameof(Index), new { search, Showlocation = ShowlocationTypeSSOT.FirstPage, MainPageContentType = MainPageContentTypeSSOT.Ticket });
        }

        [MenuFilter]
        [DisplayName("مدیریت محتوا")]
        [GroupingDashboard(GroupingDashboardSSOT.Content)]
        public IActionResult PostContent(ContentEntitySearchViewModel search)
        {

            return RedirectToAction(nameof(Index), new { search = search, Showlocation = ShowlocationTypeSSOT.Post });
        }
    }
}