using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.Entity
{
    public class Season
    {
        public int Id { get; set; }

        //عنوان
        public string SpringTitle{ get; set; }
        //توضیحات مختصر
        public string SpringShortDescription { get; set; }
        //محتوا
        public string SpringContent { get; set; }
        //تصویر
        public string SpringHeaderImage { get; set; }
        //تصویر موبایل
        public string SpringHeaderMobileImage { get; set; }

        //عنوان
        public string WinterTitle { get; set; }
        //توضیحات مختصر
        public string WinterShortDescription { get; set; }
        //محتوا
        public string WinterContent { get; set; }
        //تصویر
        public string WinterHeaderImage { get; set; }
        //تصویر موبایل
        public string WinterHeaderMobileImage { get; set; }

        //عنوان
        public string AutumnTitle { get; set; }
        //توضیحات مختصر
        public string AutumnShortDescription { get; set; }
        //محتوا
        public string AutumnContent { get; set; }
        //تصویر
        public string AutumnHeaderImage { get; set; }
        //تصویر موبایل
        public string AutumnHeaderMobileImage { get; set; }

        //عنوان
        public string SummerTitle { get; set; }
        //توضیحات مختصر
        public string SummerShortDescription { get; set; }
        //محتوا
        public string SummerContent { get; set; }
        //تصویر
        public string SummerHeaderImage { get; set; }
        //تصویر موبایل
        public string SummerHeaderMobileImage { get; set; }
    }
}
