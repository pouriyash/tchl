using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Text;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Menu;

namespace Tochal.Core.DomainModels.DTO.MenuEntity
{
    public class MenuEntityDTO : EntityModel
    {
        public int Id { get; set; }
        public int? Lang_Id { get; set; }
        public int order { get; set; } = 0;
        public int? ParentId { get; set; }
        public string video { get; set; }
        //عنوان
        [StringLength(200)]
        public string Title { get; set; }
        public string MobileImage { get; set; }
        public string HeaderImage { get; set; }

        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }

        //ردیف
        public RowSSOT Row { get; set; }

        public string Link { get; set; }

        public DetailTypeSSOT? DetailType { get; set; }

        public PageTypeSSOT PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        public bool? HaveChaild { get; set; }


    }
    public class MenuEntityTitleDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }


    }

    public class MenuEntityParentDTO
    {
        public int Id { get; set; }
        public int Lang_Id { get; set; }
        //عنوان
        [StringLength(200)]
        public string Title { get; set; }
        public string video { get; set; }
        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }
        public string HeaderImage { get; set; }
        public string MobileImage { get; set; }
        public string MainImage { get; set; }

        public string Link { get; set; }

        public DetailTypeSSOT? DetailType { get; set; }

        public PageTypeSSOT PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        public bool? HaveChaild { get; set; }


    }

    public class MenuEntityChildDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? Lang_Id { get; set; }
        public RowSSOT Row { get; set; }
        public string video { get; set; }
        //عنوان
        [StringLength(200)]
        public string Title { get; set; }
        public string MainImage { get; set; }
        public string MobileImage { get; set; }

        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }
        public string HeaderImage { get; set; }

        public string Link { get; set; }

        public DetailTypeSSOT? DetailType { get; set; }

        public PageTypeSSOT PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }



    }

    public class MenuEntityViewModel
    {
        public LangSSOT? Lang_Id { get; set; }

        public int? ParentId { get; set; }

        //عنوان
        [StringLength(200)]
        public string Title { get; set; }

        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }

        //ردیف
        public RowSSOT Row { get; set; }
        public string HeaderImage { get; set; }
        public string MobileImage { get; set; }
        public string video { get; set; }

        public string Link { get; set; }

        public DetailTypeSSOT? DetailType { get; set; }

        public PageTypeSSOT? PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        public bool HaveChild { get; set; }
    }

    public class MenuEntitySearchViewModel : PagingModel
    {

        [StringLength(200)]
        public string Title { get; set; }

        public int? ParentId { get; set; }

        public RowSSOT RowType { get; set; } = RowSSOT.First;

        public DetailTypeSSOT? DetailType { get; set; }

    }


    public class MenuEntityEditViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public RowSSOT Row { get; set; }
        //عنوان
        [StringLength(200)]
        public string Title { get; set; }
        public string HeaderImage { get; set; }
        public string MobileImage { get; set; }
        public string Video { get; set; }

        //زیر عنوان
        [StringLength(200)]
        public string SubTitle { get; set; }

        public string Link { get; set; }

        public DetailTypeSSOT? DetailType { get; set; }

        public PageTypeSSOT PageType { get; set; }

        [StringLength(500)]
        public string ExternalLink { get; set; }

        //شرح مختصر
        public string ShortContent { get; set; }

        //شرح کامل
        public string Content { get; set; }

        public bool? HaveChaild { get; set; }
    }

}
