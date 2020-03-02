using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tochal.Core.DomainModels.DTO;
using Tochal.Core.DomainModels.DTO.ContentEntity;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Menu;
using Tochal.Core.DomainModels.ViewModel;

namespace Tochal.Infrastructure.Mapping
{
    public class ContentMapping: Profile
    {
        public ContentMapping()
        {
            CreateMap<ContactUs, ContactUsSummaryDTO>().ReverseMap();
            CreateMap<ContactUs, ContactUsViewModel>().ReverseMap();

            CreateMap<Notification, NotificationSummaryDTO>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();


            CreateMap<ContentEntity, ContentEntityDTO>().ReverseMap();
            CreateMap<ContentEntity, ContentEntityViewModel>().ReverseMap();


            CreateMap<MenuEntity, MenuEntityDTO>().ReverseMap();
            CreateMap<MenuEntity, MenuEntityViewModel>().ReverseMap();
            CreateMap<MenuEntity, MenuEntityChildDTO>().ReverseMap();
            CreateMap<MenuEntity, MenuEntityParentDTO>().ReverseMap();
            CreateMap<MenuEntity, MenuEntityTitleDTO>().ReverseMap();
            CreateMap<MenuEntity, MenuEntityEditViewModel>().ReverseMap();
        }
    }
}
