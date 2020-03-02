using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Tochal.Core.DomainModels.DTO.ContentEntity;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Core.DomainModels.Entity.Menu;
using Tochal.Infrastructure.Services;
using Tochal.UI.Helpers;
using TochalUI.Models;

namespace TochalUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly MenuService _MenuRepository;
        private readonly ContentManagementService _contentManagementService;
        private readonly GalleryService _GalleryService;
        private readonly GroupGalleryService _GroupGalleryService;
        private readonly GalleryEntityService _GalleryEntityService;
        private readonly SeasonService _seasonService;
        private readonly CommentService _commentService;
        public HomeController(MenuService MenuRepository, CommentService commentService, SeasonService seasonService, GroupGalleryService GroupGalleryService, ContentManagementService contentManagementService, GalleryEntityService GalleryEntityService, GalleryService GalleryService)
        {
            _GroupGalleryService = GroupGalleryService;
            _contentManagementService = contentManagementService;
            _MenuRepository = MenuRepository;
            _GalleryService = GalleryService;
            _GalleryEntityService = GalleryEntityService;
            _seasonService = seasonService;
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            //var client = new RestClient($"http://tochalweather.herokuapp.com/top");
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = await client.ExecuteAsync(request);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Detail(int id, DetailTypeSSOT DetailType)

        {
            var Content = await _contentManagementService.Find(id);

            var GalleryEntityList = _GalleryEntityService
                .List(Tochal.Core.DomainModels.Entity.Content.GalleryTypeSSOT.Image, Tochal.Core.DomainModels.Entity.Content.GalleryContentTypeSSOT.Content, id);


            if (Content.SubMenuId.HasValue)
            {
                var menu = await _MenuRepository.Find(Content.SubMenuId.Value);
                TempData["DetailType"] = menu.DetailType;
            }

            if (DetailType == DetailTypeSSOT.Pro)
            {
                return View("~/Views/Home/PostDetail.cshtml", new Tuple<ContentEntity, List<Tochal.Core.DomainModels.Entity.Content.GalleryEntity>>(Content, GalleryEntityList));
            }
            else
            {
                var Comments = _commentService.GetByEntityId(id);
                var list = new List<Gallery>();
                foreach (var item in GalleryEntityList)
                {
                    if (item.GroupGallery != null && item.GroupGallery.Galleries.Count > 0)
                    {
                        foreach (var item3 in item.GroupGallery.Galleries)
                        {

                            list.Add(item3);
                        }
                    }
                }
                return View("~/Views/Home/Text.cshtml", new Tuple<ContentEntity, List<Tochal.Core.DomainModels.Entity.Comment>, List<Tochal.Core.DomainModels.Entity.Content.Gallery>>(Content, Comments, list));
            }
        }

        public async Task<IActionResult> Post(int menuId)
        {
            var menu = await _MenuRepository.Find(menuId);
            TempData["DetailType"] = menu.DetailType;
            var Contents = _contentManagementService.GetAllByMenuId(menuId);

            return View(Contents);
        }
        public async Task<IActionResult> Blog(int menuId)
        {
            var menu = await _MenuRepository.Find(menuId);
            TempData["DetailType"] = menu.DetailType;
            TempData["BlogTitle"] = "مطالب";
            var Contents = _contentManagementService.GetAllByMenuId(menuId);

            return View(Contents);
        }
        public async Task<IActionResult> SinglePage(int menuId)
        {
            var menu = await _MenuRepository.Find(menuId);
            var GalleryEntityList = _GalleryEntityService.List(Tochal.Core.DomainModels.Entity.Content.GalleryTypeSSOT.Image, Tochal.Core.DomainModels.Entity.Content.GalleryContentTypeSSOT.Menu, menuId);
            return View(new Tuple<MenuEntity, List<Tochal.Core.DomainModels.Entity.Content.GalleryEntity>>
                (menu, GalleryEntityList));
        }

        public async Task<IActionResult> MoreEvent()
        {
            var list = await _contentManagementService
                .GetAllByMainPageContentType(MainPageContentTypeSSOT.Event);
            TempData["BlogTitle"] = "رویدادها";
            return View(viewName: "~/Views/Home/Blog.Cshtml", model: list.Take(4).ToList());
        }

        public async Task<IActionResult> MoreNews()
        {
            var list = await _contentManagementService
                .GetAllByMainPageContentType(MainPageContentTypeSSOT.News);
            TempData["BlogTitle"] = "اخبار";
            return View(viewName: "~/Views/Home/Blog.Cshtml", model: list.ToList());
        }

        public IActionResult ImageGallery()
        {
            var list = _GroupGalleryService.List(GalleryForTypeSSOT.Gallery,Tochal.Core.DomainModels.Entity.Content.GalleryTypeSSOT.Image);
            return View(list);
        }
        public IActionResult ClipGallery()
        {
            var list = _GroupGalleryService.List(GalleryForTypeSSOT.Gallery, Tochal.Core.DomainModels.Entity.Content.GalleryTypeSSOT.Clip);
            return View(list);
        }

        public PartialViewResult Languages()
        {
            return PartialView(_MenuRepository.GetLanguages());
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddMonths(3) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult GroupGallery(int id)
        {
            var model = _GroupGalleryService.GetFull(id);
            return View(model);
        }

        public IActionResult GroupGalleryClip(int id)
        {
            var model = _GroupGalleryService.GetFull(id);
            return View(model);
        }


        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult AllSeason()
        {
            return View();
        }

        public IActionResult Spring()
        {
            var season = _seasonService.Get();
            return View(season);
        }

        public IActionResult Winter()
        {
            var season = _seasonService.Get();
            return View(season);
        }

        public IActionResult Autumn()
        {
            var season = _seasonService.Get();
            return View(season);
        }

        public IActionResult Summer()
        {
            var season = _seasonService.Get();
            return View(season);
        }

    }
}
