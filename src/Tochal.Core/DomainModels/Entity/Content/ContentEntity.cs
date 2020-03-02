using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tochal.Core.DomainModels.Entity.Menu;

namespace Tochal.Core.DomainModels.Entity.Blog
{
    public class ContentEntity
    {
        public int Id { get; set; }

        public int Lang_Id { get; set; }
         
        //عنوان
        [StringLength(300)]
        public string Title { get; set; }

        //زیر عنوان
        [StringLength(300)]
        public string SubTitle { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        //تصویر بند انگشتی
        public string MobileImage { get; set; }

        //تصویر اصلی
        public string MainImage { get; set; }
        public string MainImageThubNail { get; set; }

        public string HeaderImage { get; set; }

        //فیلم اصلی
        public string MainVideo { get; set; }

        //لینک خارجی
        [StringLength(500)]
        public string ExternalLink { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        //نوع
        public BlogContentEntityTypeSSOT? BlogContentEntityType { get; set; }

        //محتویات صفحه اصلی
        public MainPageContentTypeSSOT? MainPageContentType { get; set; }

        //جایگاه نمایشی
        public ShowlocationTypeSSOT? Showlocation { get; set; }

        //برای کدام منو
        public int? MenuId { get; set; }
        public int? MenuTitleId { get; set; }
        public int? SubMenuId { get; set; }

        #region Relations
        [ForeignKey(nameof(Lang_Id))]
        public virtual Language Language { get; set; }


        [ForeignKey(nameof(MenuId))]
        public virtual MenuEntity Menu { get; set; }

        [ForeignKey(nameof(MenuTitleId))]
        public virtual MenuEntity MenuTitle { get; set; }

        [ForeignKey(nameof(SubMenuId))]
        public virtual MenuEntity SubMenu { get; set; }
        #endregion
    }

    public enum BlogContentEntityTypeSSOT
    {
        [Display(Name = "متن")]
        Text,
        [Display(Name = "عکس")]
        Image,
        [Display(Name = "فیلم")]
        Video
    }

    public enum ShowlocationTypeSSOT
    {
        [Display(Name = "صفحه اصلی")]
        FirstPage,
        [Display(Name = "مطالب")]
        Post
    }

    public enum MainPageContentTypeSSOT
    {
        [Display(Name = "جدیدترین اخبار")]
        News,
        [Display(Name = "اسلایدر")]
        Slider,
        [Display(Name = "پربازدیدها")]
        MostVist,
        [Display(Name = "رویداد ویژه")]
        SpecialEvent,
        [Display(Name = "رویداد")]
        Event,
        [Display(Name = "خرید آنلاین")]
        Ticket
    }

    public class PageTypeEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

    }

    public class ActionViewType
    {
        public int Id { get; set; }

        public BlogContentEntityTypeSSOT Type { get; set; }

        public string ActionName { get; set; }
    }
}
