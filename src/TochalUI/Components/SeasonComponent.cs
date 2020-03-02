using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using Tochal.Infrastructure.Services;

namespace TochalUI.Components
{
    [AllowAnonymous]
    public class Season : ViewComponent
    { 
        private readonly SeasonService _SeasonService;
        public Season(SeasonService SeasonService)
        {
            _SeasonService = SeasonService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int numberToTake)
        {
            var model = _SeasonService.Get();
            return View(viewName: "Default", model: model);
        }

    }
}
