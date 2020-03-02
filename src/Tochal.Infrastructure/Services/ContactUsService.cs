using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tochal.Core.DomainModels.DTO;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Content;
using Tochal.Core.DomainModels.ViewModel;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.GenericService;

namespace Tochal.Infrastructure.Services
{

    public class ContactUsService
    {
        private readonly ApplicationDbContext _context;

        public ContactUsService(ApplicationDbContext context)
        { 
            _context = context;
        }

        //public IPaginated<ContactUsSummaryDTO> GetAll(ContactUsSearchViewModel search)
        //{
        //    var model = _context.ContactUs
        //        .WhereIf(!string.IsNullOrEmpty(search.Term), q => q.Title.Contains(search.Term))
        //        .ProjectTo<ContactUsSummaryDTO>()
        //        .OrderByDescending(c => c.Id)
        //         .ToPaginated(new PaginatedCriteria(search.Page, search.PageSize));

        //    return model;
        //}

        public List<ContactUs> GetAll()
        {
            var model = _context.ContactUs
                .ToList();

            return model;
        }


        public ServiceResult<int> Create(ContactUs model)
        {
            _context.ContactUs.Add(model);
            _context.SaveChanges();
            var result = ServiceResult<int>.Okay(model.Id);
            return result;
        }
         
    }

}
