using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.DomainModels.Entity.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity;
using DNTBreadCrumb.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using DNTCommon.Web.Core;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity.Emails;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Web.Services.Contracts.Identity;
using Research.City.Admin.Toolkit;

namespace Tochal.Web.Areas.Identity.Controllers
{
    [Authorize]
    [Area(AreaConstants.IdentityArea)] 
    public class ChangePasswordController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IUsedPasswordsService _usedPasswordsService;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

        public ChangePasswordController(
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IEmailSender emailSender,
            IPasswordValidator<User> passwordValidator,
            IUsedPasswordsService usedPasswordsService,
            IOptionsSnapshot<SiteSettings> siteOptions)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _signInManager = signInManager;
            _signInManager.CheckArgumentIsNull(nameof(_signInManager));

            _passwordValidator = passwordValidator;
            _passwordValidator.CheckArgumentIsNull(nameof(_passwordValidator));

            _usedPasswordsService = usedPasswordsService;
            _usedPasswordsService.CheckArgumentIsNull(nameof(_usedPasswordsService));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _siteOptions = siteOptions;
            _siteOptions.CheckArgumentIsNull(nameof(_siteOptions));
        }

         public async Task<IActionResult> Index()
        {
            var userId = this.User.Identity.GetUserId<int>();
            var passwordChangeDate = await _usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId);
            return View(model: new ChangePasswordViewModel
            {
                LastUserPasswordChangeDate = passwordChangeDate
            });
        }

        public async Task<IActionResult> ChangePassword(int userId)
        {
             var passwordChangeDate = await _usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var token =await _userManager.GeneratePasswordResetTokenAsync(user);
            return View(model: new ChangeUserPasswordViewModel(){ userId= userId,Token= token });
        }
        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePassword(string newPassword)
        {
            var user = await _userManager.GetCurrentUserAsync();
            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, user, newPassword);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetCurrentUserAsync();
            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);

                // reflect the changes in the Identity cookie
                await _signInManager.RefreshSignInAsync(user);

                await _emailSender.SendEmailAsync(
                           email: user.Email,
                           subject: "اطلاع رسانی تغییر کلمه‌ی عبور",
                           viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_ChangePasswordNotification.cshtml",
                           model: new ChangePasswordNotificationViewModel
                           {
                               User = user,
                               EmailSignature = _siteOptions.Value.Smtp.FromName,
                               MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                           });

                return RedirectToAction(nameof(Index), "UserCard", routeValues: new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.userId.ToString());
            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token,model.NewPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);

                return Utility.CloseAndRefresh();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

    }
}