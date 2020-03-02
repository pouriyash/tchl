using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class GalleryService : GenericService<Gallery>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Gallery> table = null;

        public GalleryService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<Gallery>();

        }

        //public void Create(List<string> Images, int entityId, GalleryTypeSSOT GalleryType)
        //{
        //    var list = new List<Gallery>();

        //    foreach (var item in Images)
        //    {
        //        list.Add(new Gallery {
        //            EntityId=entityId,
        //            Image=item,
        //            GalleryType= GalleryType
        //        });
        //    }
        //    _context.Gallery.AddRange(list);
        //    _context.SaveChanges();
        //}

        //public void RemoveAll(int entityId, GalleryTypeSSOT GalleryType)
        //{
        //    var list = table.Where(p => p.GalleryType == GalleryType && p.EntityId == entityId).ToList();
        //    _context.Gallery.RemoveRange(list);
        //    _context.SaveChanges();

        //}

        public void SetFirstImage(int id, int GroupGalleryId)
        {

            var FirstExist = _context.Gallery.Where(p => p.GroupGalleryId == GroupGalleryId && p.FirstImage == true).FirstOrDefault();

            if (FirstExist != null)
            {
                FirstExist.FirstImage = false;
                _context.Gallery.Update(FirstExist);

            }

            var model = _context.Gallery.Find(id);
            model.FirstImage = true;
            _context.Gallery.Update(model);

            var groupGallery = _context.GroupGallery.Find(GroupGalleryId);
            groupGallery.FirstImageId = id;
            _context.GroupGallery.Update(groupGallery);

            _context.SaveChanges();
        }

        public void RemoveFirstImage(int GroupGalleryId)
        {

            var FirstExist = _context.Gallery.Where(p => p.GroupGalleryId == GroupGalleryId && p.FirstImage == true).ToList();

            if (FirstExist.Count() > 0)
            {
                var list = FirstExist.Select(q => q.GroupGalleryId).ToList();

                var groupGallery = _context.GroupGallery.Where(p => list.Contains(p.Id)).ToList();

                foreach (var item in groupGallery)
                {
                    item.FirstImageId = null;
                    _context.GroupGallery.Update(item);
                    _context.SaveChanges();

                }

                _context.Gallery.RemoveRange(FirstExist);
                _context.SaveChanges();
            }

        }

        public void RemoveAllByGroupGalleryId(int GroupGalleryId)
        {
            var list = _context.Gallery.Where(p => p.GroupGalleryId == GroupGalleryId).ToList();
            _context.Gallery.RemoveRange(list);
            _context.SaveChanges();
        }

        public void EditText(int id, string Alt)
        {
            var model = _context.Gallery.Find(id);
            model.Alt = Alt;

            _context.Gallery.Update(model);
            _context.SaveChanges();
        }

    }

}
