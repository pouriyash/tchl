﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    /// <summary>
    /// توضیحات اضافه عملیات
    /// F1, F2 ... F7
    /// </summary>
    public class Routine2Action
    {
        /// <summary>
        /// شناسه منحصربه‌فرد روال
        /// </summary>
        public int RoutineId { get; set; }


        /// <summary>
        /// شماره مرحله
        /// </summary>
        public int Step { get; set; }


        /// <summary>
        /// عملیاتی که انجام شده است
        /// F1, ... F7
        /// </summary>
        [MaxLength(32)]
        public string Action { get; set; }


        /// <summary>
        /// عنوان عملیات نظیر ارسال یا تایید
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string Title { get; set; }

        [MaxLength(64)]
        public string Icon { get; set; }

        [MaxLength(64)]
        public string Color { get; set; }


        /// <summary>
        /// آیا پرکردن باکس توضیحات اجباری است یا خیر
        /// </summary>
        public bool IsDescriptionRequired { get; set; }

        /// <summary>
        /// آیا باکس توضیحات را باید پنهان کند یا خیر
        /// true = پنهان کند
        /// false = نمایش دهد
        /// </summary>
        public bool ShouldHideDescription { get; set; } = false;


        /// <summary>
        /// آیا باکس توضیحات باید چندخطی باشد یا تک خطی
        /// false = Textbox = تک خطی
        /// true = Textarea = چندخطی
        /// </summary>
        public bool IsDescriptionMultiline { get; set; }


        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}
