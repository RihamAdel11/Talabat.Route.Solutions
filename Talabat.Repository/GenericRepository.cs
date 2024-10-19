using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepositry<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Product))
                return (IReadOnlyList<T>)await _dbContext.Set<Product>().Include(p=>p.Brand ).Include (C=>C.Category ).ToListAsync();
            return await _dbContext .Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
           return await ApplaySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))

                return await _dbContext.Set<Product>().Where(p=>p.Id==id).Include(p => p.Brand).Include(C => C.Category).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync() ;
        }
        private IQueryable<T> ApplaySpecification(ISpecification <T>spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);

        }
        public void Add(T entity)
        => _dbContext.Set<T>().Add(entity);

        public void Delete(T entity)
        => _dbContext.Set<T>().Update(entity);
        public void Update(T entity)
        => _dbContext.Set<T>().Remove(entity);
    }
}
