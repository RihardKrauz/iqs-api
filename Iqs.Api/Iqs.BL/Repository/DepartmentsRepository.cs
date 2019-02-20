using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Repository
{
    public class DepartmentsRepository : GenericRepository<Department>, IDepartmentsRepository
    {
        public DepartmentsRepository(BaseContext dbContext) : base(dbContext) {  }

    }
}
