using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class EditUserInfoViewModel
    {
        public int Id { get; set; }
        [StringLength(450)]
        public string FirstName { get; set; }

        [StringLength(450)]
        public string LastName { get; set; }

        [StringLength(450)]
        public string CompanyName { get; set; }

        [StringLength(450)]
        [Display(Name = "نام دانشکاه:")]
        public string UniversityName { get; set; }

        [StringLength(450)]
        [Display(Name = "نام رئیس دانشگاه:")]
        public string UniversityManagerName { get; set; }

        [Display(Name = "نام فارسی واحد صنعتی:")]
        [StringLength(450)]
        public string CompanyNameFa { get; set; }

        public string FatherName { get; set; }

        public bool? Gender { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Fax { get; set; }

        public string Tell { get; set; }

        public bool? IsIrainian { get; set; }

        public int ProvinceId { get; set; }

        public int CityId { get; set; }

        public int? UniTypeId { get; set; }

        public bool? IsAcceptCompany { get; set; }

        public bool? IsAcceptUniversity { get; set; }

        public virtual string PhoneNumber { get; set; }
        public string CallNumber { get; set; }

        public virtual string Email { get; set; }

        public virtual string UserName { get; set; }

        public string NationalCode { get; set; }
    }
}
