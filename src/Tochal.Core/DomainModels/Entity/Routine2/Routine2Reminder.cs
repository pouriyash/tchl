﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Routine2Reminder
    {
        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// شماره مرحله ای که طرح ها در آن مرحله باید چک شوند
        /// </summary>
        public int Step { get; set; }



        /// <summary>
        /// مدت زمانی که پس از گذشت آن
        /// باید پیغام ارسال شود
        /// </summary>
        public int TimeoutDays { get; set; }



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



        [MaxLength(32)]
        public string Key { get; set; }



        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}