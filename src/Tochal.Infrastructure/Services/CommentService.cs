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

    public class CommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        { 
            _context = context;
        }
         
        public List<Comment> GetAll()
        {
            var model = _context.Comment
                .ToList();

            return model;
        }

        public Comment GetById(int id)
        {
            var model = _context.Comment
                .FirstOrDefault(p=>p.Id==id);

            return model;
        }

        public List<Comment> GetByEntityId(int EntityId)
        {
            var model = _context.Comment
                .Where(p=>p.EntityId == EntityId&&p.IsShow==true).OrderByDescending(p=>p.Id).ToList();

            return model;
        }

        public List<Comment> GetAllByEntityId(int EntityId)
        {
            var model = _context.Comment
                .Where(p=>p.EntityId == EntityId).OrderByDescending(p=>p.Id).ToList();

            return model;
        }

        public ServiceResult Create(CommentViewModel model)
        {
            var entity = new Comment();
            entity.Name = model.Name;
            entity.Text = model.Text;
            entity.EntityId = model.EntityId;
            _context.Comment.Add(entity);
            _context.SaveChanges();
            var result = ServiceResult.Okay();
            return result;
        }

        public ServiceResult<int> Edit(Comment model)
        {
            var entity = _context.Comment.Find(model.Id);

            Mapper.Map(model, entity);

            _context.Comment.Update(entity);
            _context.SaveChanges();
            var result = ServiceResult<int>.Okay(model.Id);
            return result;
        }
          
        public ServiceResult EnableShow(int id,bool status)
        {
            var entity = _context.Comment.Find(id);

            entity.IsShow = status;

            _context.Comment.Update(entity);
            _context.SaveChanges();
            var result = ServiceResult.Okay();
            return result;

        }

        public void Delete(int id)
        {
            var entity = _context.Comment.Find(id);
             
            _context.Comment.Remove(entity);
            _context.SaveChanges();
        }

        public ServiceResult Update(int id,string Answer,String Text,String Name,bool IsShow)
        {
            var entity = _context.Comment.Find(id);
            entity.Answer = Answer;
            entity.Text = Text;
            entity.Name = Name;
            entity.IsShow = IsShow;
            _context.Comment.Update(entity);
            _context.SaveChanges();
            var result = ServiceResult.Okay();
            return result;

        }

    }

}
