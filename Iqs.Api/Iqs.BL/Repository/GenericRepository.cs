using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T :  class, IEntity
    {
        private readonly BaseContext _dbContext;

        public GenericRepository(BaseContext dbContext) {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll() {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> Get(Func<T, bool> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().Where(predicate).AsQueryable();
        }

        public async Task<T> GetById(long id) {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> Create(T entity) {
            var entry = await _dbContext.Set<T>().AddAsync(entity);
            return entry.Entity;
        }

        public void Update(long id, T entity) {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task Delete(long id) {
            var entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
