using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class SmsChangePasswordViewModel
    {
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "تلفن همراه")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد.")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشند.", MinimumLength = 6)]
    
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌ی عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد.")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه‌ی عبور جدید")]
        [System.ComponentModel.DataAnnotations.Compare(nameof(NewPassword), ErrorMessage = "کلمات عبور وارد شده با هم تطابق ندارند")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
