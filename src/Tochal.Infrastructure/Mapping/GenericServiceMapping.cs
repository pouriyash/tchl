using AutoMapper;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Core.DomainModels.ViewModel;

namespace Tochal.Infrastructure.Mapping
{
    public class GenericServiceMapping : Profile
    {
        public GenericServiceMapping()
        {
            CreateMap<GroupGallery, GroupGalleryDTO>().ReverseMap();
            CreateMap<GroupGallery, GroupGalleryViewModel>().ReverseMap();
            CreateMap<Gallery, GalleryDTO>().ReverseMap();
            CreateMap<Gallery, GalleryViewModel>().ReverseMap();

            CreateMap<GalleryEntity, GalleryEntityDTO>().ReverseMap();
            CreateMap<GalleryEntity, GalleryEntityViewModel>().ReverseMap();

        }
    }
}
