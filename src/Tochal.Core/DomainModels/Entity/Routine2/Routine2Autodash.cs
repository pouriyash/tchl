﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Routine2Autodash
    {
        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// شماره مرحله شروع
        /// طرح‌هایی که قرار است کارتابلشان عوض شود
        /// بر اساس این شماره مرحله بررسی می‌شوند
        /// </summary>
        public int Step { get; set; }


        /// <summary>
        /// شماره مرحله مقصد
        /// </summary>
        public int ToStep { get; set; }


        /// <summary>
        /// مدت زمانی که پس از گذشت آن
        /// طرح باید به کارتابل جدید انتقال پیدا کند
        /// </summary>
        public int TimeoutDays { get; set; }


        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}