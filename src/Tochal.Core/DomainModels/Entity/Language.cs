using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tochal.Core.DomainModels.Entity
{
    public class Language
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string LanguageTitle{ get; set; }
    }

    public enum LangSSOT
    {
        [Display(Name="فارسی")]
        fa=1,
        [Display(Name="انگلیسی")]
        En=2
    }
}
