using Alamut.Data.Structure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class SeasonService
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Season> table = null;

        public SeasonService(ApplicationDbContext context) 
        {
            _context = context;
            table = _context.Set<Season>();

        }

        public Season Get()
        {
            var seasons = _context.Season.FirstOrDefault();
            return seasons;
        }

        public void create(Season season)
        {
            table.Add(season);
            var status=_context.SaveChanges();

        }

        public ServiceResult UpdateAutumn(Season season)
        {
            if (season.Id==0|| season.Id<0)
            {
                create(season);
            }
            else
            {
                var model = table.Find(season.Id);
                if (model!=null)
                { 
                    model.AutumnContent = season.AutumnContent;
                    model.AutumnHeaderImage = season.AutumnHeaderImage;
                    model.AutumnHeaderMobileImage = season.AutumnHeaderMobileImage;
                    model.AutumnShortDescription = season.AutumnShortDescription;
                    model.AutumnTitle = season.AutumnTitle;
                    table.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    create(season);
                }
            }
            return ServiceResult.Okay();
        }

        public ServiceResult UpdateSpring(Season season)
        {
            if (season.Id == 0 || season.Id < 0)
            {
                create(season);
            }
            else
            {
                var model = table.Find(season.Id);
                if (model != null)
                { 
                    model.SpringContent = season.SpringContent;
                    model.SpringHeaderImage = season.SpringHeaderImage;
                    model.SpringHeaderMobileImage = season.SpringHeaderMobileImage;
                    model.SpringShortDescription = season.SpringShortDescription;
                    model.SpringTitle = season.SpringTitle;
                    table.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    create(season);
                }
            }
            return ServiceResult.Okay();
        }

        public ServiceResult UpdateSummer(Season season)
        {
            if (season.Id == 0 || season.Id < 0)
            {
                create(season);
            }
            else
            {
                var model = table.Find(season.Id);
                if (model != null)
                { 
                    model.SummerContent = season.SummerContent;
                    model.SummerHeaderImage = season.SummerHeaderImage;
                    model.SummerHeaderMobileImage = season.SummerHeaderMobileImage;
                    model.SummerShortDescription = season.SummerShortDescription;
                    model.SummerTitle = season.SummerTitle;
                    table.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    create(season);
                }
            }
            return ServiceResult.Okay();
        }

        public ServiceResult UpdateWinter(Season season)
        {
            if (season.Id == 0 || season.Id < 0)
            {
                create(season);
            }
            else
            {
                var model = table.Find(season.Id);
                if (model != null)
                {
                    Mapper.Map(season, model);
                    model.WinterContent = season.WinterContent;
                    model.WinterHeaderImage = season.WinterHeaderImage;
                    model.WinterHeaderMobileImage = season.WinterHeaderMobileImage;
                    model.WinterShortDescription = season.WinterShortDescription;
                    model.WinterTitle = season.WinterTitle;
                    table.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    create(season);
                }
            }
            return ServiceResult.Okay();
        }




    }

}
