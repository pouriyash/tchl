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
using Tochal.Core.DomainModels.ViewModel;
using Alamut.Data.Paging;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Infrastructure.Services.Contracts.Identity;

namespace Tochal.Web.Controllers.Banks
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("مدیریت کامنت ها")]
    public class CommentController : Controller
    {
        private readonly CommentService _CommentService;
        private readonly ContentManagementService _contentManagementService;

        public CommentController(
            
            CommentService CommentService,
            ContentManagementService contentManagementService
            )
        {
            _contentManagementService = contentManagementService;
            _CommentService = CommentService; 
        }

        [DisplayName("مدیریت کامنت ها ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public async Task<IActionResult> Index()
        {
            var data = _CommentService.GetAll();
            var contentList =await _contentManagementService.GetAllContentWithComment(data.Select(p => p.EntityId).ToList());
            return View(contentList);
        }

        public async Task<IActionResult> CommentList(int entityId)
        {
            var data = _CommentService.GetAllByEntityId(entityId);
             return View(data);
        }

        public IActionResult IsShow(int id,bool status)
        {
            _CommentService.EnableShow(id,status);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _CommentService.Delete(id);
            TempData.AddResult(ServiceResult.Okay());
            return Utility.CloseAndRefresh();
        }

        public IActionResult Update(int id)
        {
            var model = _CommentService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id,string answer,string Name, string Text,bool IsShow)
        {
            _CommentService.Update(id, answer, Text, Name, IsShow);
            TempData.AddResult(ServiceResult.Okay());
            return Utility.CloseAndRefresh();
        }
         
    }


}