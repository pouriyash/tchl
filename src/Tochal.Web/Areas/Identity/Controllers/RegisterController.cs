using Tochal.Core.Common.GuardToolkit;
using Tochal.Core.Common.IdentityToolkit;

using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

using DNTPersianUtils.Core;
using DNTCommon.Web.Core;
using Tochal.Core.DomainModels.Entity.Identity;
using Tochal.Infrastructure.Services.Contracts.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Core.DomainModels.ViewModel.Identity;
using Tochal.Core.DomainModels.ViewModel.Identity.Emails;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tochal.Infrastructure.Services;
using Tochal.Infrastructure.Services.Identity;
using Tochal.Core.DomainModels.SSOT;
using AutoMapper;
using Alamut.Data.Structure;
using Research.City.Admin.Toolkit;
using Tochal.Infrastructure.Services.Contracts;
 
namespace Tochal.Web.Areas.Identity.Controllers
{
    [Area(AreaConstants.IdentityArea)]
    [AllowAnonymous]
    [BreadCrumb(Title = "ثبت نام", UseDefaultRouteUrl = true, Order = 0)]
    public class RegisterController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger<RegisterController> _logger;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IUserValidator<User> _userValidator;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
        private readonly UserService _userService;
        private readonly ISmsManagement _smsService;
 
        public RegisterController(
            IApplicationUserManager userManager,
            IPasswordValidator<User> passwordValidator,
            IUserValidator<User> userValidator,
            IApplicationSignInManager signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IOptionsSnapshot<SiteSettings> siteOptions,
            ILogger<RegisterController> logger,
            UserService userService,
            ISmsManagement smsService
         )
        {
            _smsService = smsService;
            _smsSender = smsSender;
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _passwordValidator = passwordValidator;
            _passwordValidator.CheckArgumentIsNull(nameof(_passwordValidator));

            _userValidator = userValidator;
            _userValidator.CheckArgumentIsNull(nameof(_userValidator));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));

            _siteOptions = siteOptions;
            _siteOptions.CheckArgumentIsNull(nameof(_siteOptions));

            _userService = userService;
            _signInManager = signInManager;

         }

        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidateUsername(string username)
        {
            //یکتا باشد
            var IsExist = await _userService.FindUserByUserName(username);

            return Json(!IsExist);
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidateEmail(string email)
       {
            //یکتا باشد
            var IsExist = await _userService.FindUserByEmail(email);

            return Json(!IsExist);
        } 
        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePassword(string password, string username)
        {
            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, new User { UserName = username }, password);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }


        //TODO 
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePhoneNumber(string password, string PhoneNumber)
        {
            var errors = new List<IdentityError>();

            if (!PhoneNumber.IsValidIranianMobileNumber())
            {
                errors.Add(new IdentityError
                {
                    Description = "فرمت شماره موبایل وارد شده نادرست است.",
                    Code = "500"
                });
            }

            var IsExistPhoneNumber = _userManager.IsExistUserByPhoneNumber(PhoneNumber);
            if (IsExistPhoneNumber)
            {
                errors.Add(new IdentityError
                {
                    Description = "شماره تلفن  وارد شد در سامانه موجود است.",
                    Code = "500"
                });
            }
            var result = errors.Any()
                           ? IdentityResult.Failed(errors.ToArray())
                           : IdentityResult.Success;

            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidateNationalCode(string NationalCode)
        {
            var errors = new List<IdentityError>();

            if (!NationalCode.IsValidIranianNationalCode())
            {
                errors.Add(new IdentityError
                {
                    Description = "فرمت کدملی وارد شده اشتباه است.",
                    Code = "500"
                });
            }

            if (_userService.FindUserByNationalCode(NationalCode))
            {
                errors.Add(new IdentityError
                {
                    Description = "کدملی در سامانه موجود است.",
                    Code = "500"
                });
            }

            var result = errors.Any()
                           ? IdentityResult.Failed(errors.ToArray())
                           : IdentityResult.Success;

            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [BreadCrumb(Title = "تائید ایمیل", Order = 1)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        public IActionResult Index(bool? CreateByAdmin, UserTypeSSOT UserType)
        {
            if (CreateByAdmin.HasValue)
                ViewBag.CreateByAdmin = CreateByAdmin.Value;

            ViewBag.UserType = UserType;

            return View();
        }

        [BreadCrumb(Title = "تائیدیه ایمیل", Order = 1)]
        public IActionResult ConfirmedRegisteration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateDNTCaptcha(CaptchaGeneratorLanguage = DNTCaptcha.Core.Providers.Language.Persian)]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();

                Mapper.Map(model, user);

                if (model.UserType == UserTypeSSOT.Person)
                {
                    user.UserName = model.NationalCode;
                }
                else
                {
                    user.UserName = model.Email;
                }

                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"کاربر {user.UserName} از طریق سایت ثبت نام کرده است.");

                    var ChangePhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);

                    _smsService.sendMessage(model.PhoneNumber, "به سامانه کارآموزی خوش آمدید.");
                    //var smsResult = await _smsSender.SendSmsAsync(model.PhoneNumber, $"برای تکمیل ثبت نام خود کد : {ChangePhoneNumberToken} را وارد نمایید.");

                    if (model.UserType == UserTypeSSOT.Person)
                    {
                        await _userManager.AddToRoleAsync(user, ConstantRoles.UserBase);
                    }
                     
                    if (model.CreateByAdmin.HasValue && model.CreateByAdmin == true)
                    {
                        return Redirect("~/Identity/UsersManager/Index");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, false, null);
                        return Redirect("/");
                    }
                }
                List<string> errors=new List<string>();

                foreach (var error in result.Errors)
                {
                    //ModelState.AddModelError(string.Empty, error.Description);
                    errors.Add(error.Description);
                }

                TempData.AddResult(ServiceResult.Error(string.Join("<br/>", errors)));
            }

            ViewBag.UserType = model.UserType;
            return View(model);
        }

        [BreadCrumb(Title = "ایمیل خود را تائید کنید", Order = 1)]
        public IActionResult ConfirmYourEmail()
        {
            return View();
        }

        public IActionResult EmtpyComfirmCompany(string name,string compnayName)
        {
            ViewBag.name = name;
            ViewBag.compnayName = compnayName;

            return View();
        }
         
    }
}