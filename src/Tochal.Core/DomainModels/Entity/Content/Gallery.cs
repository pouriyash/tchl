using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tochal.Core.DomainModels.DTO;

namespace Tochal.Core.DomainModels.Entity.Content
{
    public class Gallery
    {
        public int Id { get; set; }

        public int GroupGalleryId { get; set; }

        public bool? FirstImage { get; set; }

        public string Alt { get; set; }

        public string Image { get; set; } 
    }

    public class GroupGallery
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }

        public GalleryTypeSSOT GalleryType { get; set; } = GalleryTypeSSOT.Image;
        public GalleryForTypeSSOT GalleryForType { get; set; } = GalleryForTypeSSOT.Gallery;

        public int? FirstImageId { get; set; }

        [ForeignKey(nameof(FirstImageId))]
        public virtual Gallery Gallery { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
    }

  

    public class GalleryEntity
    {
        public int Id { get; set; }

        public int galleryGroupId { get; set; }

        public int EntityId { get; set; }

        public GalleryTypeSSOT GalleryType { get; set; } = GalleryTypeSSOT.Image;
        public GalleryContentTypeSSOT GalleryContentType { get; set; } = GalleryContentTypeSSOT.Content;

        [ForeignKey(nameof(galleryGroupId))]
        public virtual GroupGallery GroupGallery { get; set; }
    }


    public class GalleryEntityDTO : EntityModel
    {
        public int Id { get; set; }

        public int galleryGroupId { get; set; }

        public int EntityId { get; set; }

        public GalleryTypeSSOT GalleryType { get; set; }
    }

    public enum GalleryTypeSSOT
    {
        Clip,
        Image
    }

    public enum GalleryContentTypeSSOT
    {
        Menu,
        Content
    }

    public enum GalleryForTypeSSOT
    {
        Gallery,
        Content
    }

    public class GalleryEntityViewModel
    {
        public int Id { get; set; }

        public int galleryGroupId { get; set; }

        public int EntityId { get; set; }

        public GalleryTypeSSOT GalleryType { get; set; }
        public GalleryContentTypeSSOT GalleryContentType { get; set; }

    }


    public class GroupGallerySearchViewModel : PagingModel
    { 
        public string Title { get; set; }

    }

    public class GroupGalleryDTO : EntityModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }
        public GalleryTypeSSOT GalleryType { get; set; }
        public GalleryForTypeSSOT GalleryForType { get; set; } = GalleryForTypeSSOT.Gallery;

    }


    public class GroupGalleryViewModel
    {
        public GalleryTypeSSOT GalleryType { get; set; }
        public GalleryForTypeSSOT GalleryForType { get; set; } = GalleryForTypeSSOT.Gallery;

        public string Title { get; set; }
        public string Image { get; set; }
    }


    public class GallerySearchViewModel : PagingModel
    { 
        public int? FirstImageGalleryId { get; set; }
        public int GroupGalleryId { get; set; }
        public string Alt { get; set; }

        public string Image { get; set; }
    }

    public class GalleryDTO : EntityModel
    {
        public int Id { get; set; }

        public int GroupGalleryId { get; set; }
        public bool? FirstImage { get; set; }
        public string Alt { get; set; }

        public string Image { get; set; }
    }


    public class GalleryViewModel
    {
        public int Id { get; set; }
        public int GroupGalleryId { get; set; }
        public int? FirstImageGalleryId { get; set; }

        public string Alt { get; set; }

        public string Image { get; set; }
    }
}
