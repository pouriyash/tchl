﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Tochal.Core.DomainModels.Entity.Identity;

 namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Routine2Log
    {
        public int Id { get; set; }


        /// <summary>
        /// شماره منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// شماره منحصر به فرد طرح
        /// </summary>
        public int EntityId { get; set; }


        /// <summary>
        /// کاربری که سبب ایجاد لاگ شده است
        /// برای استفاده داخلی سیستم
        /// مثلا دبیرخانه برای شخصی طرح ثبت میکند
        /// شناسه کاربر دبیرخانه در اینجا ذخیره می‌شود
        /// شناسه کاربر اصلی که طرح برایش ثبت شده است در فیلد UserId
        /// </summary>
        public int? CreatorUserId { get; set; }



        /// <summary>
        /// کاربر
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// تاریخ انجام عملیات
        /// </summary>
        public DateTime ActionDate { get; set; }


        /// <summary>
        /// شماره مرحله فعلی که عملیات بر روی آن انجام شده است
        /// مثلا وقتی استاد طرحی را از مرحله ۱ ارسال میکند و به مرحله ۲۰۰ میرود
        /// شماره مرحله ۱ در این فیلد قرار میگیرد
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// شماره مرحله نهایی که طرح پس از انجام عملیات به آن مرحله می‌رود
        /// مثلا وقتی استاد طرحی را از مرحله ۱ ارسال میکند و به مرحله ۲۰۰ میرود
        /// شماره مرحله ۲۰۰ در این فیلد قرار میگیرد
        /// </summary>
        public int? ToStep { get; set; }


        /// <summary>
        /// توضیح در صورت وجود
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// عملیاتی که انجام شده است
        /// F1, F2, F3, F4, F5, F6, F7
        /// </summary>
        [MaxLength(1024)]
        public string Action { get; set; }


        /// <summary>
        /// مقدار فیلد
        /// Routine2Role.DashboardEnum
        /// که کارتابل فعلی را در برمیگیرد
        /// یکسری موارد پیش فرض هم داریم که وابسته به آن جدول ‌می‌تواند نباشد
        /// که در
        /// Routine2Roles.PredefinedDashboardsSSOT
        /// ذخیره می‌شود
        /// نظیر
        /// _Moderator
        /// که می‌شود مدیر ارشد سیستم
        /// </summary>
        [MaxLength(1024)]
        public string RoutineRoleTitle { get; set; }


        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; } 
    }
}
