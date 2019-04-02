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
    public class GradesRepository : GenericRepository<Grade>, IGradesRepository
    {
        private readonly BaseContext _dbContext;
        public GradesRepository(BaseContext dbContext) : base(dbContext) { _dbContext = dbContext; }

        public IQueryable<Grade> GetBySpecializationId(long specId)
        {
            return _dbContext.Set<Grade>()
                .AsNoTracking()
                .Where(e => e.SpecializationId == specId)
                .AsQueryable();
        }
    }
}
