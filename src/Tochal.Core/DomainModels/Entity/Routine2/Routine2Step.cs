﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    /// <summary>
    /// مراحل مختلف روال
    /// </summary>
    public class Routine2Step
    {
        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// عنوان مرحله
        /// مثل
        /// در انتظار بررسی توسط دبیرخانه
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string Title { get; set; }


        /// <summary>
        /// شماره مرحله
        /// </summary>
        public int Step { get; set; }

        
        /// <summary>
        /// عملیات مختلف روال که در اثر آن طرح
        /// از یک مرحله به مرحله دیگر میرود
        /// این موارد برای هر روال متفاوت است و وابسته به
        /// ENUM
        /// همان روال می‌باشد
        /// </summary>
        public int? F1 { get; set; }
        public int? F2 { get; set; }
        public int? F3 { get; set; }
        public int? F4 { get; set; }
        public int? F5 { get; set; }
        public int? F6 { get; set; }
        public int? F7 { get; set; }




        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}
