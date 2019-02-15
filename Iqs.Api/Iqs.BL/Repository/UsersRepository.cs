using Iqs.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Iqs.BL.Interfaces;
using Iqs.BL.Contexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Iqs.BL.Repository
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
