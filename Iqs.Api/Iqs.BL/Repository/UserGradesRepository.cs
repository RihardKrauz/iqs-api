using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Repository
{
    public class UserGradesRepository : GenericRepository<UserGrade>, IUserGradesRepository
    {
        public UserGradesRepository(BaseContext dbContext) : base(dbContext) { }
    }
}
