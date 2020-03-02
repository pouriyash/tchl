﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    /// <summary>
    /// کارتابل‌های مختلف روال
    /// نظیر استاد، دانشگاه، دبیرخانه
    /// </summary>
    public class Routine2Role
    {
        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// عنوان کارتابل
        /// مثل
        /// استاد
        /// دانشگاه
        /// دبیرخانه
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }


        /// <summary>
        /// مراحل کارتابل
        /// که به صورت
        /// JSON
        /// ذخیره خواهد شد
        /// و برای نمایش در «تازه‌ها» مورد استفاده قرار میگیرد
        /// مثلا
        /// ["200", "210", "220", "230"]
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string StepsJson { get; set; }



        /// <summary>
        /// Enum
        /// متناظر با داشبورد مرتبط با روال
        /// مثل
        /// Applicant
        /// Colleague
        /// University
        /// Secretariat
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string DashboardEnum { get; set; }


        /// <summary>
        /// ترتیب کارتابل برای رسم مجدد دیاگرام
        /// </summary>
        public int SortOrder { get; set; }





        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}
