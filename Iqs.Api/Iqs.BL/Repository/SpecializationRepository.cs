using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Repository
{
    public class SpecializationsRepository : GenericRepository<Specialization>, ISpecializationsRepository
    {
        public SpecializationsRepository(BaseContext dbContext) : base(dbContext) {  }

    }
}
