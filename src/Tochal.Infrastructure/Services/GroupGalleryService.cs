using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class GroupGalleryService : GenericService<GroupGallery>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<GroupGallery> table = null;

        public GroupGalleryService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<GroupGallery>();

        }

        public List<GroupGallery> List(GalleryForTypeSSOT GalleryForType, GalleryTypeSSOT GalleryType)
        {
            var list = _context.GroupGallery.Where(p=>p.GalleryType== GalleryType).Where(p => p.GalleryForType == GalleryForType).Include(p=>p.Gallery).ToList();
            return list;
        }

        public List<GroupGallery> ListByType(GalleryTypeSSOT GalleryType)
        {
            var list = _context.GroupGallery.Where(p=>p.GalleryType== GalleryType&&p.GalleryForType==GalleryForTypeSSOT.Gallery).Include(p=>p.Gallery).ToList();
            return list;
        }

        public GroupGallery GetFull(int id)
        {
            var list = _context.GroupGallery.Where(p=>p.Id==id).Include(p=>p.Galleries).FirstOrDefault();
            return list;
        }
 
    }

}
