using Alamut.Data.Structure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tochal.Core.DomainModels.DTO;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{
    public class FooterService
    {
        private readonly ApplicationDbContext _context;
        private DbSet<FooterInfo> table = null;

        public FooterService(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<FooterInfo>();
        }

        public FooterFullDTO GetInfo()
        {
            var FooterInfo = _context.FooterInfo.FirstOrDefault();
            var FooterColleague = _context.FooterColleague.ToList();

            if (FooterInfo == null)
                FooterInfo = new FooterInfo();
             
            if (FooterColleague == null)
                FooterColleague = new List<FooterColleague>();

            return new FooterFullDTO
            {
                FooterInfo = FooterInfo,
                FooterColleague = FooterColleague
            };
        }

        public ServiceResult UpdateInfo(FooterInfo footerInfo/*, List<FooterColleague> footerColleagues*/)
        {
            _context.FooterInfo.Update(footerInfo);
            _context.SaveChanges(); 

            //footerColleagues.ForEach(p => p.FooterId = footerInfo.Id);

            //_context.FooterColleague.UpdateRange(footerColleagues);

            _context.SaveChanges();

            return ServiceResult.Okay();
        }

    }

}
