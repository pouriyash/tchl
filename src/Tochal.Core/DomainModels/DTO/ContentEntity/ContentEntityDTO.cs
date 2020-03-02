using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Text;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;

namespace Tochal.Core.DomainModels.DTO.ContentEntity
{
    public class ContentEntityDTO: EntityModel
    {
        public int Id { get; set; }
        //عنوان
        [StringLength(300)]
        public string Title { get; set; }
        public string MobileImage { get; set; }

        //زیر عنوان
        [StringLength(300)]
        public string SubTitle { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        public int? Lang_Id { get; set; }

        //تصویر بند انگشتی
        public string ThumbnailImage { get; set; }

        //تصویر اصلی
        public string MainImage { get; set; }

        //فیلم اصلی
        public string MainVideo { get; set; }

        //لینک خارجی
        [StringLength(500)]
        public string ExternalLink { get; set; }

        //نوع
        public BlogContentEntityTypeSSOT? BlogContentEntityType { get; set; }
        public MainPageContentTypeSSOT MainPageContentType { get; set; }

        //جایگاه نمایشی
        public ShowlocationTypeSSOT Showlocation { get; set; }

        //برای کدام منو
        public int? MenuId { get; set; }
        public int? MenuTitleId { get; set; }
        public int? SubMenuId { get; set; }

        //تصویر هدر
        public string HeaderImage { get; set; }

    }

    public class ContentEntityViewModel
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public LangSSOT Lang_Id { get; set; }
        //عنوان
        [StringLength(300)]
        public string Title { get; set; }
        public string MobileImage { get; set; }

        //زیر عنوان
        [StringLength(300)]
        public string SubTitle { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        //تصویر بند انگشتی
        public string ThumbnailImage { get; set; }

        //تصویر اصلی
        public string MainImage { get; set; }

        //فیلم اصلی
        public string MainVideo { get; set; }

        //تصویر هدر
        public string HeaderImage { get; set; }

        //لینک خارجی
        [StringLength(500)]
        public string ExternalLink { get; set; }

        //نوع
        public BlogContentEntityTypeSSOT? BlogContentEntityType { get; set; }

        //جایگاه نمایشی
        public ShowlocationTypeSSOT? Showlocation { get; set; }

        //محتویات صفحه اصلی
        public MainPageContentTypeSSOT? MainPageContentType { get; set; }

        //برای کدام منو
        public int? MenuId { get; set; }
        public int? MenuTitleId { get; set; }
        public int? SubMenuId { get; set; }
        //[UploadFileExtensions("", ErrorMessage = "لطقا فایل ویدیویی آپلود کنید.")]
        //[DataType(DataType.Upload)]
        //public IFormFile mainVideoFile { get; set; }

        //[UploadFileExtensions(".png,.jpg,.jpeg,.gif", ErrorMessage = "لطفا فایل عکسی آپلود کنید.")]
        //[DataType(DataType.Upload)]
        //public IFormFile mainImageFile { get; set; }

        //[UploadFileExtensions(".png,.jpg,.jpeg,.gif", ErrorMessage = "لطفا فایل عکسی آپلود کنید.")]
        //[DataType(DataType.Upload)]
        //public List<IFormFile> galleryImageFile { get; set; }

    }

    public class ContentEntitySearchViewModel: PagingModel
    {
        public string Title { get; set; }
        public ShowlocationTypeSSOT Showlocation { get; set; }
        public MainPageContentTypeSSOT? MainPageContentType { get; set; }
    }
}
