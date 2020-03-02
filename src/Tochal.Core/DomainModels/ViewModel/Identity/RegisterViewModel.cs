using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tochal.Core.DomainModels.SSOT;
using Microsoft.AspNetCore.Mvc;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class RegisterViewModel : IValidatableObject
    {
        //[Remote("ValidateUsername", "Register",
        //    AdditionalFields = nameof(Username) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST", ErrorMessage = "این نام کاربری از قبل در سیستم وجود دارد.")]
        //[Display(Name = "نام کاربری")]
        //[RegularExpression("^[a-zA-Z_]*$", ErrorMessage = "لطفا تنها از حروف انگلیسی استفاده نمائید")]
        //public string Username { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [Display(Name = "نام")]
        [StringLength(450)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
                          ErrorMessage = "لطفا تنها از حروف فارسی استفاده نمائید")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [Display(Name = "نام خانوادگی")]
        [StringLength(450)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
                          ErrorMessage = "لطفا تنها از حروف فارسی استفاده نمائید")]
        public string LastName { get; set; }

        [Display(Name = "نام شرکت")]
        [StringLength(450)]
        public string CompanyName { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [Remote("ValidateEmail", "Register",
            AdditionalFields = nameof(Email) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST", ErrorMessage = "این ایمیل از قبل در سیستم وجود دارد.")]
        [EmailAddress(ErrorMessage = "لطفا آدرس ایمیل معتبری را وارد نمائید.")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        //[Remote("ValidateEmail", "Register",
        //    AdditionalFields = nameof(mail) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST", ErrorMessage = "این ایمیل از قبل در سیستم وجود دارد.")]
        //[EmailAddress(ErrorMessage = "لطفا آدرس ایمیل معتبری را وارد نمائید.")]
        //[Display(Name = "ایمیل")]
        //public string mail { get; set; }

        [Remote("ValidateRegistrationNumber", "Register",
            AdditionalFields = nameof(CityId) + "," + nameof(ProvinceId) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST", ErrorMessage = "این شماره ثبت برای این شهر از قبل در سیستم وجود دارد.")]
        [Display(Name = "شماره ثبت")]
        public string registrationNumber { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشند.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌ی عبور")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه‌ی عبور")]
        [Compare(nameof(Password), ErrorMessage = "کلمات عبور وارد شده با هم تطابق ندارند")]
        public string ConfirmPassword { get; set; }

        [Remote("ValidatePhoneNumber", "Register",
    AdditionalFields = nameof(PhoneNumber) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
        [DataType(DataType.PhoneNumber)]
        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        [Display(Name = "تلفن همراه")]
        public string PhoneNumber { get; set; }

        [Remote("ValidateNationalCode", "Register",
    AdditionalFields = nameof(NationalCode) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
        [Display(Name = "کدملی")]
        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        public string NationalCode { get; set; }

        //[Display(Name = "شناسه ملی")]
        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        public string CompanyNationalId { get; set; }

        /// <summary>
        /// شناسه ملی مرکز اصلی
        /// </summary>
        //[Display(Name = "شناسه ملی مرکز اصلی")]
        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        public string CompanyNationalMainId { get; set; }

        /// <summary>
        /// نام مرکز اصلی
        /// </summary>
        [Display(Name = "نام مرکز اصلی")]
        public string CompanyNationalMainName { get; set; }

        public UserTypeSSOT UserType { get; set; }
        public bool? CreateByAdmin { get; set; }  
        [Display(Name = "تلفن ثابت")]
        public string CallNumber { get; set; }
     
        public string StartDate { get; set; }
        public string ManagerName { get; set; }
        //نام مسئول کارآموزی
        public string ExpertTochal { get; set; }
        public int? UniTypeId { get; set; }
        [StringLength(450)]
        [Display(Name = "نام دانشکاه:")]
        public string UniversityName { get; set; }

        [StringLength(450)]
        [Display(Name = "نام رئیس دانشگاه:")]
        public string UniversityManagerName { get; set; }

        [Display(Name = "نامه تایید صنعت:")]
        public string CompanyConfirm { get; set; }

        public string UniManagerName { get; set; }
        [Display(Name ="نمابر")]
        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        public string Fax { get; set; }
        public bool? IsIrainian { get; set; }
        public bool? Gender { get; set; } 
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public string IndustryRelationsManagerName { get; set; }
        public string IndustryRelationsManagerEmail { get; set; }

        public bool? IsAcceptCompany { get; set; } = false;

        //[Required(ErrorMessage = "(*) این فیلد باید پر شود")]
        public string Address { get; set; }

        public string WebSite { get; set; }

        [Display(Name = "نام انگلیسی واحد صنعتی:")]
        [StringLength(450)]
        public string CompanyNameEn { get; set; }
        public bool? IsAcceptUniversity { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if(UserType == UserTypeSSOT.Person)
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    errors.Add(new ValidationResult("لطفا نام را وارد کنید", new List<string>() { nameof(FirstName) }));
                }

                if (string.IsNullOrWhiteSpace(LastName))
                {
                    errors.Add(new ValidationResult("لطفا نام خانوادگی را وارد کنید", new List<string>() { nameof(LastName) })); 
                }

                if (string.IsNullOrWhiteSpace(NationalCode))
                {
                    errors.Add(new ValidationResult("لطفا کد ملی را وارد کنید", new List<string>() { nameof(NationalCode) }));
                }
            }

            else if (UserType == UserTypeSSOT.Company)
            {
                if (string.IsNullOrWhiteSpace(CompanyName))
                {
                    errors.Add(new ValidationResult("لطفا نام شرکت را وارد کنید", new List<string>() { nameof(CompanyName) }));
                }

                if (string.IsNullOrWhiteSpace(CompanyNationalId))
                {
                    errors.Add(new ValidationResult("لطفا شناسه ملی را وارد کنید", new List<string>() { nameof(CompanyNationalId)}));
                }
            }

            return errors;
        }
    }
}
