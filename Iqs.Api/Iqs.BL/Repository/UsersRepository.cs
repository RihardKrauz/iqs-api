using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Contexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Iqs.DAL.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {

        private readonly BaseContext _dbContext;

        public UsersRepository(BaseContext dbContext) : base(dbContext) { _dbContext = dbContext; }

        public async Task<User> GetByLogin(string login)
        {
            return await _dbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Login == login);
        }
    }
}
