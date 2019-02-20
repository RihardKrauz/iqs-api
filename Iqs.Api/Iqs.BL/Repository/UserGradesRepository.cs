using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iqs.DAL.Repository
{
    public class UserGradesRepository : GenericRepository<UserGrade>, IUserGradesRepository
    {
        private readonly BaseContext _dbContext;
        public UserGradesRepository(BaseContext dbContext) : base(dbContext) { _dbContext = dbContext; }
        public async Task<Grade> GetCurrentGradeForUser(User user)
        {
            return await (from ug in _dbContext.Set<UserGrade>().AsNoTracking()
                 join g in _dbContext.Set<Grade>().AsNoTracking() on ug.GradeId equals g.Id
                 where ug.UserId == user.Id
                 orderby ug.QualifiedDate
                 select g).FirstOrDefaultAsync();
        }

        public IEnumerable<Grade> GetGradesForUser(User user)
        {
            return (from ug in _dbContext.Set<UserGrade>().AsNoTracking()
                          join g in _dbContext.Set<Grade>().AsNoTracking() on ug.GradeId equals g.Id
                          where ug.UserId == user.Id
                          select g).AsEnumerable();
        }
    }
}
