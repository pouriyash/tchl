﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Routine2Notice
    {

        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }
        public int FromStep { get; set; }
        public int ToStep { get; set; }

        [MaxLength(32)]
        public string Key { get; set; }

        /// <summary>
        /// عنوان پیغام / برای استفاده داخلی
        /// </summary>
        [MaxLength(512)]
        public string Title { get; set; }

        /// <summary>
        /// متن پیغام که قرار است ارسال شود
        /// به صورت Notice
        /// </summary>
        public string Body { get; set; }


        /// <summary>
        /// متن پیغامی که قرار است پیامک شود
        /// </summary>
        public string BodySms { get; set; }


        /// <summary>
        /// متن پیغامی که قرار است ایمیل شود
        /// </summary>
        public string BodyEmail { get; set; }


        /// <summary>
        /// کوئری اسکیوال که قرار است اجرا شود
        /// و خروجی آن لیستی از اعداد خواهد بود که پیغام
        /// به آنها ارسال خواهد شد
        /// </summary>
        public string UserIdsSqlQuery { get; set; }


        /// <summary>
        /// کوئری که باید به دیتابیس بزنیم و از آن دیتای مورد نظر را استخراج کنیم
        /// </summary>
        public string ModelSqlQuery { get; set; }


        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}
