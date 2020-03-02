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
    [DisplayName("مدیریت گالری")]
    public class ContactUsController : Controller
    {
        private readonly ContactUsService _ContactUsService;

        public ContactUsController(
            
            ContactUsService ContactUsService
            )
        { 
            _ContactUsService = ContactUsService; 
        }

        [DisplayName("لیست ارتباط با ما ")]
        [GroupingDashboard(GroupingDashboardSSOT.Banks)]
        public IActionResult Index(ContactUsSearchViewModel search)
        {
            var data = _ContactUsService.GetAll();
            //var model = new SearchCriteriaPageModel<IPaginated<ContactUsSummaryDTO>, ContactUsSearchViewModel>(data, search);
            return View(data);
        }
               

    }


}