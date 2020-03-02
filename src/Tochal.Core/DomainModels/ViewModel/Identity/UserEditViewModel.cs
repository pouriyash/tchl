using Tochal.Core.DomainModels.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalCode { get; set; }

        public string CompanyNationalId { get; set; }

        public string CompanyNationalMainId { get; set; }

        public string CompanyNationalMainName { get; set; }

        public UserTypeSSOT UserType { get; set; }
        
        public bool? CreateByAdmin { get; set; }
        
        public string CallNumber { get; set; }
         
        public string StartDate { get; set; }
        
        public string ManagerName { get; set; }
        
        public string ExpertTochal { get; set; }
       
        public int? UniTypeId { get; set; }
        
        public string UniversityName { get; set; }

        public string UniversityManagerName { get; set; }
        
        public string UniManagerName { get; set; }
        
        public string Fax { get; set; }
        
        public bool? IsIrainian { get; set; }
        
        public bool? Gender { get; set; }
        
        public int CityId { get; set; }
        
        public int ProvinceId { get; set; }
        
        public string IndustryRelationsManagerName { get; set; }
        
        public string IndustryRelationsManagerEmail { get; set; }

        public bool? IsAcceptCompany { get; set; } = false;
        
        public string Address { get; set; }

        public bool? IsAcceptUniversity { get; set; } = false;
    }
}
