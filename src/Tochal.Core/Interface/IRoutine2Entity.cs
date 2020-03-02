using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tochal.Core.Interface
{
    public class IRoutine2Entity
    {
        /// <summary>
        /// مرحله فعلی روال
        /// برابر با فیلد [RoutineStep].[Step]
        /// </summary>
        public int RoutineStep { get; set; }

        /// <summary>
        /// آیا طرح وارد روال شده است یا خیر
        /// از این فیلد برای تفکیک کارتابل پیش‌نویس استفاده می‌کنیم
        /// </summary>
        public bool RoutineIsFlown { get; set; }

        /// <summary>
        /// آیا طرح به اتمام رسیده است یا خیر
        /// از این فیلد برای تفکیک کارتابل خاتمه‌یافته‌ها استفاده می‌کنیم
        /// </summary>
        public bool RoutineIsDone { get; set; }

        /// <summary>
        /// تاریخ اولیه به جریان افتادن روال
        /// این فیلد فقط بار اول پر می‌شود و دیگر آپدیت نمی‌شود
        /// </summary>
        public DateTime? RoutineFlownDate { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر کارتابل روال
        /// با هر بار تغییر کارتابل روال، این فیلد آپدیت می‌شود
        /// </summary>
        public DateTime? RoutineStepChangeDate { get; set; }

        /// <summary>
        /// آیا طرح با موفقیت همراه بوده است یا خیر
        /// طرح یا تایید نهایی می‌شود یا رد نهایی
        /// در کارتابل خاتمه‌یافته‌ها این طرح‌ها را فیلتر گذاری میکنیم
        /// </summary>
        public bool RoutineIsSucceeded { get; set; }

        /// <summary>
        /// شماره مراحلی که فرآیند در آنها خاتمه یافته است
        /// </summary>
        [NotMapped]
        public List<int> DoneSteps { get; set; } = new List<int>();

        /// <summary>
        /// شماره مراحلی که فرآیند در آنها موفق بوده است
        /// </summary>
        [NotMapped]
        public List<int> SucceededSteps { get; set; }
    }
}
