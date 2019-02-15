using Iqs.BL.Contexts;
using Iqs.BL.Interfaces;
using Iqs.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Repository
{
    public class UserGradesRepository : GenericRepository<UserGrade>, IUserGradesRepository
    {
        public UserGradesRepository(BaseContext dbContext) : base(dbContext) { }
    }
}
