using Iqs.BL.Contexts;
using Iqs.BL.Interfaces;
using Iqs.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Repository
{
    public class GradesRepository : GenericRepository<Grade>, IGradesRepository
    {
        public GradesRepository(BaseContext dbContext) : base(dbContext) { }
    }
}
