using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc; 
using Tochal.Core.DomainModels.DTO.ContentEntity;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Menu;
using Tochal.Infrastructure.Services;
using Tochal.UI.Helpers;
using TochalUI.Models;

namespace TochalUI.Controllers
{
    [AllowAnonymous]
    public class ContactusController : Controller
    {
        private readonly ContactUsService _contactUsService;
        public ContactusController(ContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactUs contactUs)
        {
            _contactUsService.Create(contactUs);
            return RedirectToAction(nameof(Index));
        }
         
         
    }
}
