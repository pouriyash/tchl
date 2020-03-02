using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Tochal.Core.DomainModels.Entity;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services.Contracts.Genericservice;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tochal.Core.DomainModels.DTO;

namespace Tochal.Infrastructure.Services.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class,new()
    {

        #region Dependencies
        private readonly IUnitOfWork _context;
        #endregion

        private DbSet<T> table = null;

        #region constructor
        public GenericService(IUnitOfWork context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        #endregion

        public ServiceResult Create<TViewModel>(TViewModel newEntity)where TViewModel : class
        {
            var entity = Mapper.Map<T>(newEntity);

            table.Add(entity);

            if (_context.SaveChanges() > 0)
                return ServiceResult.Okay();
            return ServiceResult.Error();
        }

        public async Task<ServiceResult> Delete(int id)
        {

            var entity =await Find(id);

            table.Remove(entity);
           
            if (_context.SaveChanges() > 0)
                return ServiceResult.Okay();
            return ServiceResult.Error();
        }

        public async Task<ServiceResult> Edit<TModel>(int id, TModel editedEntity) where TModel:class
        {
            var oldEntity =await Find(id);
            Mapper.Map<TModel,T>(editedEntity, oldEntity);
             
            table.Update(oldEntity);

            if (_context.SaveChanges() > 0)
                return ServiceResult.Okay();
            return ServiceResult.Error();
        }

        public bool ExistById(int id)
        {
            var entity = table.Find(id);

            if (entity != null)
                return true;
            return false;
        }

        public async Task<List<TModel>> GetAll<TModel>(List<Expression<Func<TModel, bool>>> predicates=null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null) where TModel : EntityModel
        {
            IQueryable<TModel> query = null;
            query=table.ProjectTo<TModel>();

            if (predicates != null)
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate);
                }
            }
            

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TModel> GetByCondition<TModel>(Expression<Func<T,bool>> where = null) where TModel : class
        {
            return await table.WhereIf(where != null, where).ProjectTo<TModel>().FirstOrDefaultAsync();
        }

        public async Task<T> Find(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<int> TotalCount()
        {
            return await table.CountAsync();
        }
    }
}
