using Alamut.Data.Structure;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tochal.Core.DomainModels.DTO.MenuEntity;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Menu;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{

    public class MenuService : GenericService<MenuEntity>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<MenuEntity> table = null;
        private DbSet<Language> Language = null;

        public MenuService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<MenuEntity>();
            Language = _context.Set<Language>();

        }

        public ServiceResult<int> Add(MenuEntityViewModel newEntity)
        {

            var entity = Mapper.Map<MenuEntityViewModel, MenuEntity>(newEntity);
            table.Add(entity);

            if (_context.SaveChanges() > 0)
                return ServiceResult<int>.Okay(data: entity.Id);
            return ServiceResult<int>.Error("خطایی در هنگام ثبت اطلاعات رخ داده است.لطفا دوباره تلاش کنید.");
        }

        public List<Language> GetLanguages()
        {
            var CurrentLang = CultureInfo.CurrentCulture.Name;
            return Language.Where(p => p.LanguageTitle != CurrentLang).ToList();
        }
        public List<MenuEntityTitleDTO> ApiGetAll()
        {
            string lang = CultureInfo.CurrentCulture.Name;
            return table
                .Where(p => p.Language.LanguageTitle == lang)
                .ProjectTo<MenuEntityTitleDTO>()
                .OrderBy(q => q.Title)
                .ToList();
        }
        public async Task<List<MenuEntityDTO>> GetList()
        {
            string lang = CultureInfo.CurrentCulture.Name;
            return table
                .Where(p => p.Language.LanguageTitle == lang && p.Row == RowSSOT.First || p.Row != RowSSOT.First)
                .ProjectTo<MenuEntityDTO>()
                .OrderBy(q => q.order)
                .ToList();
        }

        public List<MenuEntityTitleDTO> ApiGetParents()
        {
            return table
                .Where(q => q.ParentId == null && q.Row == RowSSOT.First && q.PageType == PageTypeSSOT.List)
                .ProjectTo<MenuEntityTitleDTO>()
                .OrderBy(q => q.Title)
                .ToList();
        }

        public List<MenuEntityTitleDTO> ApiGetTitles(int ParentId)
        {
            return table
                .Where(q => q.Row == RowSSOT.Title && q.ParentId == ParentId)
                .ProjectTo<MenuEntityTitleDTO>()
                .OrderBy(q => q.Title)
                .ToList();
        }

        public List<MenuEntityTitleDTO> ApiGetChilds(int parentId)
        {
            return table
                .Where(q => q.ParentId == parentId && q.Row == RowSSOT.Second)
                .ProjectTo<MenuEntityTitleDTO>()
                .OrderBy(q => q.Title)
                .ToList();
        }


    }

}
