using Alamut.Data.Structure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tochal.Core.DomainModels.DTO.ContentEntity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class ContentManagementService : GenericService<ContentEntity>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<ContentEntity> table = null;

        public ContentManagementService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<ContentEntity>();

        }

        public BlogContentEntityTypeSSOT? GetTypeOfViewDetail(string ActionName)
        {
            var ActionViewType = _context.ActionViewType.Where(p => p.ActionName == ActionName).FirstOrDefault();
            if (ActionViewType != null)
                return ActionViewType.Type;
            return null;
        }

        public async Task<List<ContentEntity>> GetAllByMainPageContentType(MainPageContentTypeSSOT MainPageContentType)
        {
            string lang = CultureInfo.CurrentCulture.Name;

            var list =await table.Where(p => p.MainPageContentType == MainPageContentType && p.Language.LanguageTitle == lang).OrderByDescending(p=>p.Id).ToListAsync();
            return list;
        }

        public async Task<List<ContentEntity>> GetAllContentWithComment(List<int> ids)
        {
            string lang = CultureInfo.CurrentCulture.Name;

            var list =await table.Where(p => ids.Contains(p.Id)).ToListAsync();
            return list;
        }

        public List<ContentEntity> GetAllByMenuId(int MenuId)
        {
            string lang = CultureInfo.CurrentCulture.Name;

            var list = table.Where(p => p.SubMenuId == MenuId&& p.Language.LanguageTitle == lang).ToList();
            return list;
        }

        public ServiceResult<int> Add(ContentEntityViewModel newEntity)
        {
            
            var entity = Mapper.Map<ContentEntityViewModel, ContentEntity>(newEntity);
            table.Add(entity);

            if (_context.SaveChanges() > 0)
                return ServiceResult<int>.Okay(data:entity.Id);
            return ServiceResult<int>.Error("خطایی در هنگام ثبت اطلاعات رخ داده است.لطفا دوباره تلاش کنید.");
        }
    }

}
