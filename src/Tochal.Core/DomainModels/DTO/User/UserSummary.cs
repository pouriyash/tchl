using System;

namespace Tochal.Core.DomainModels.DTO.User
{
    public class UserSummary
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        
        public string NationalCode { get; set; }
        
        public string Email { get; set; }
        
        public string ProfessionalTitle { get; set; }
        
        public bool IsMale { get; set; }
        
        public DateTime? RegisterDate { get; set; }
        
        public bool IsScienceCommittee { get; set; }
        
        public string CompanyName { get; set; }

        public int ProvinceId { get; set; }

        public int CityId { get; set; }
        
        public int? UniversityId { get; set; }

        public string UniversityName { get; set; }

        public string PhoneCall { get; set; }
        
        public string PhoneNumber { get; set; }

        public string FatherName { get; set; }
        
        public string IdNumber { get; set; }
    }
}
