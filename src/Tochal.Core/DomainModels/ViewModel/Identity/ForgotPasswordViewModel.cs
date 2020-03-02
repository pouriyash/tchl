using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "تلفن همراه")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="لطفا کد ارسالی را وارد نمایید.")]
        public int RandomCode { get; set; }

    }
}