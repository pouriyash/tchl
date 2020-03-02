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
    public class Footer : ViewComponent
    {
        private readonly FooterService _FooterRepository;
        public Footer(FooterService FooterRepository)
        {
            _FooterRepository = FooterRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int numberToTake)
        {
            var list = _FooterRepository.GetInfo();
            return View(viewName: "Default", model: list);
        }
    }
}
