using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class GalleryEntityService : GenericService<GalleryEntity>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<GalleryEntity> table = null;

        public GalleryEntityService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<GalleryEntity>();

        }

        public List<GalleryEntity> List(GalleryTypeSSOT GalleryType, GalleryContentTypeSSOT GalleryContentType, int entityId)
        {
            var list = _context.GalleryEntity.Include(p=>p.GroupGallery).ThenInclude(p=>p.Galleries).Include(p=>p.GroupGallery).ThenInclude(p=>p.Gallery)
                .Where(p=>p.GalleryType== GalleryType&&p.GalleryContentType== GalleryContentType && p.EntityId== entityId).ToList();
            return list;
        }

        public List<GalleryEntity> GetAllList()
        {
            var list = _context.GalleryEntity.ToList();
            return list;
        }

        public void RemoveAll(GalleryTypeSSOT GalleryType, GalleryContentTypeSSOT GalleryContentType, int entityId)
        {
            var list = _context.GalleryEntity.Where(p=>p.EntityId== entityId&& p.GalleryType == GalleryType&& p.GalleryContentType == GalleryContentType).ToList();
            _context.GalleryEntity.RemoveRange(list);
            _context.SaveChanges();
        }

        public void RemoveByGroupGalleryId(GalleryTypeSSOT GalleryType, GalleryContentTypeSSOT GalleryContentType, int galleryGroupId)
        {
            var list = _context.GalleryEntity.Where(p=>p.galleryGroupId == galleryGroupId && p.GalleryType == GalleryType&& p.GalleryContentType == GalleryContentType).ToList();
            _context.GalleryEntity.RemoveRange(list);
            _context.SaveChanges();
        }

    }

}
