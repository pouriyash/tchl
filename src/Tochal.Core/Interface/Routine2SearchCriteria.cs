using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.SSOT;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace Tochal.Core.Interface
{
    public class Routine2SearchCriteria: PageModel
    {
        // فیلتر بر اساس اینکه طرح جاری شده است
        //‌ مورد استفاده برای پیش نویس : false
        public bool RoutineIsFlown { get; set; } = true;



        // لیستی از مراحل که فیلتر طرح‌ها بر اساس آن انجام خواهد شد
        // مورد استفاده برای داشبورد تازه‌ها : New
        public List<int> RoutineStepList { get; set; }


        // لیست طرح‌هایی که کاربر بر روی آن‌ها کاری انجام داده است
        // مورد استفاده برای داشبورد ارسال شده ها : Archived
        public List<int> RoutineLogList { get; set; }


        // نوع کارتابل: پیش‌نویس، ارسال شده، تازه، خاتمه یافته
        public DashboardTypes DashboardType { get; set; }

        public int? Step { get; set; }

        // فیلتر بر اساس طرح‌های به اتمام رسیده
        // مورد استفاده برای داشبورد Done
        public bool? RoutineIsDone { get; set; }

    }
}