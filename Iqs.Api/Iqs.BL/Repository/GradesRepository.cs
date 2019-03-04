using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Repository
{
    public class GradesRepository : GenericRepository<Grade>, IGradesRepository
    {
        private readonly BaseContext _dbContext;
        public GradesRepository(BaseContext dbContext) : base(dbContext) { _dbContext = dbContext; }

        public async Task<Specialization> GetBySpecializationId(long specId)
        {
            return await _dbContext.Set<Specialization>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == specId);
        }
    }
}
