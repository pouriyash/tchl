using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tochal.Core.DomainModels.Entity.Menu
{
    public class MenuEntity
    {
        public int Id { get; set; }
        public int? Lang_Id { get; set; }
        public int order { get; set; } = 0;
        public int? ParentId { get; set; }

        //عنوان
        [StringLength(200)]
        public string Title { get; set; }

        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }

        //ردیف
        public RowSSOT Row { get; set; }

        //تصویر هدر
        public string HeaderImage { get; set; }
        public string MobileImage { get; set; }

        public string video { get; set; }
        public string Link { get; set; }

        //نوع نمایش صفحه
        public DetailTypeSSOT? DetailType { get; set; }

        //نوع صفحه
        public PageTypeSSOT PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        //مخصوص پدرا است
        //برای اینکه بدونیم فرزند دارد یا خیر
        public bool? HaveChaild{ get; set; }

        [ForeignKey(nameof(Lang_Id))]
        public virtual Language Language { get; set; }
    }

    public enum DetailTypeSSOT
    {
        [Display(Name = "ساده")]
        Simple,
        [Display(Name = "بلاگ")]
        Blog,
        [Display(Name = "پیشرفته")]
        Pro
    }

    public enum PageTypeSSOT
    {
        [Display(Name = "تک صفحه")]
        Single,
        [Display(Name = "لیستی")]
        List,
        [Display(Name = "لینک خارجی")]
        ExternalLink
    }

    public enum RowSSOT
    {
        [Display(Name = "پدر")]
        First,

        [Display(Name = "عنوان")]
        Title,

        [Display(Name = "فرزند")]
        Second
    }
}
