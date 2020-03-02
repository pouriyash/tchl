using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Tochal.Core.DomainModels.Entity.AuditableEntity;
using Tochal.Core.DomainModels.SSOT; 

namespace Tochal.Core.DomainModels.Entity.Identity
{

    public class User : IdentityUser<int>, IAuditableEntity
    {
        public User()
        {
            UserUsedPasswords = new HashSet<UserUsedPassword>();
            UserTokens = new HashSet<UserToken>();
        }

        [StringLength(450)]
        [Display(Name = "نام :",Prompt = "نام")]
        public string FirstName { get; set; }

        [StringLength(450)]
        [Display(Name = "نام خانوادگی:", Prompt = "نام خانوادگی")]

        public string LastName { get; set; }
         
        public string FatherName { get; set; }
        [Display(Name = "جنسیت:")]

        public bool? Gender { get; set; }

        [Display(Name = "آدرس:", Prompt = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "نمابر:", Prompt = "نمابر")]

        public string Fax { get; set; }
        [Display(Name = "تلفن ثابت:")]

        public string Tell { get; set; }
           
        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
               

                return displayName;
            }
        }
          
        [StringLength(450)]
        public string PhotoFileName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }

        public DateTimeOffset? LastVisitDateTime { get; set; }

        public bool IsEmailPublic { get; set; }

        public string Location { set; get; }

        public bool IsActive { get; set; } = true;

        public int? RandomCode { get; set; }

        [Display(Name = "کد ملی:", Prompt = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "تلفن ثابت:", Prompt = "تلفن ثابت")]
        public string CallNumber { get; set; } 
        public string ActivityField { get; set; } 
        public string StartDate { get; set; }
       
        public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<UserLogin> Logins { get; set; }

        public virtual ICollection<UserClaim> Claims { get; set; }

        

        [Display(Name = "ایمیل:", Prompt = "ایمیل")]
        public override string Email { get; set; }

        [Display(Name = "تلفن همراه:", Prompt = "تلفن همراه")]
        public override string PhoneNumber { get; set; }
         
    }
}