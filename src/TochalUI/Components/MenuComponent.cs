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
    public class Menu: ViewComponent
    {
        private readonly MenuService _menuService;
        public Menu(MenuService MenuRepository)
        {
            _menuService = MenuRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int numberToTake)
        {
            var menus =await _menuService.GetList();

            return View(viewName: "Default", model: menus);
        }
    }
}
