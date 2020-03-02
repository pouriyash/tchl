using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

using DNTPersianUtils.Core;
using DNTCommon.Web.Core;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Tochal.Core.DomainModels.Entity.Identity;

using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;

using Tochal.Core.DomainModels.ViewModel.Identity.Emails;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Core.DomainModels.ViewModel.Identity;
using System.ComponentModel;
using Tochal.Web.Helpers;
using Tochal.Infrastructure.Services;
using Alamut.Data.Structure;
using Research.City.Admin.Toolkit;
using Tochal.Infrastructure.Services.Contracts;

namespace Tochal.Web.Areas.Identity.Controllers
{
    [Area(AreaConstants.IdentityArea)]
    [AllowAnonymous]
    [BreadCrumb(Title = "بازیابی کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
    [IsShowOnDashboard(true)]
    public class ForgotPasswordController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserManager _userManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
        private readonly ISmsManagement _smsService;
        private readonly ISmsSender _smsSender;
        private readonly UserService _userService;

        public ForgotPasswordController(
            IApplicationUserManager userManager,
            IPasswordValidator<User> passwordValidator,
            IEmailSender emailSender,
            ISmsManagement smsManagement,
            ISmsSender smsSender,
            UserService userService,
            IOptionsSnapshot<SiteSettings> siteOptions)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));
            _userService = userService;
            _passwordValidator = passwordValidator;
            _smsService = smsManagement;
            _smsSender = smsSender;
            _passwordValidator.CheckArgumentIsNull(nameof(_passwordValidator));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _siteOptions = siteOptions;
            _siteOptions.CheckArgumentIsNull(nameof(_siteOptions));
        }

        [BreadCrumb(Title = "تائید کلمه‌ی عبور فراموش شده", Order = 1)]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        [IsShowOnDashboard(true)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePassword(string password, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json("ایمیل وارد شده معتبر نیست.");
            }

            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, user, password);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(CaptchaGeneratorLanguage = DNTCaptcha.Core.Providers.Language.Persian)]
        public IActionResult Index(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByNationalCode(model.NationalCode);

                if (user == null)
                {
                    TempData.AddResult(ServiceResult.Error("کدملی وارد شده در سامانه وجود ندارد."));
                    return View("Error");
                }

                if (user.PhoneNumber.Trim() != model.PhoneNumber.Trim())
                {
                    TempData.AddResult(ServiceResult.Error("تلفن همراه وارد شده با تلفن همراه این کدملی در سامانه مقایرت دارد."));
                    return RedirectToAction(nameof(Index));
                }

                var randomCode = _userService.GenerateRandomNumber(10000, 99999);

                user.RandomCode = randomCode;
                _userService.UpdateUser(user);


                _smsService.sendMessage(model.PhoneNumber, $"<<سامانه کارآموزی>>\n \n" +
                    $"کد فعال سازی: {randomCode}");

                return RedirectToAction(nameof(ForgetPasswordSmsConfirmation), model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ForgetPasswordSmsConfirmation(ForgotPasswordViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgetPasswordConfirmation(ForgotPasswordViewModel model)
        {
            var user = _userService.GetUserByNationalCode(model.NationalCode);

            if (user.RandomCode == model.RandomCode)
            {
                return RedirectToAction(nameof(ChangePassword), model);
            }

            else
            {
                TempData.AddResult(ServiceResult.Error("کد وارد شده صحیح نمی‌باشد."));
                return View(nameof(ForgetPasswordSmsConfirmation), model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(ForgotPasswordViewModel vm)
        {
            var user = _userService.GetUserByNationalCode(vm.NationalCode);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var model = new SmsChangePasswordViewModel()
            {
                Code = token,
                NationalCode = vm.NationalCode,
                PhoneNumber = vm.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(SmsChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserByNationalCode(model.NationalCode);

            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);

                TempData.AddResult(ServiceResult.Okay());

                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [BreadCrumb(Title = "تغییر کلمه‌ی عبور", Order = 1)]
        [IsShowOnDashboard(true)]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [BreadCrumb(Title = "تائیدیه تغییر کلمه‌ی عبور", Order = 1)]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}