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
    public class CommentController : Controller
    {
        private readonly CommentService _CommentService;
        public CommentController(CommentService CommentService)
        {
            _CommentService = CommentService;
        }
         
        [HttpPost]
        public void Create(CommentViewModel Comment)
        {
            _CommentService.Create(Comment);
        }

        public IActionResult CommentList(int entityId)
        {
            _CommentService.GetByEntityId(entityId);
            return View();
        }
         
         
    }
}
