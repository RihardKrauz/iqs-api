using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IGradesRepository : IGenericRepository<Grade>
    {
        IQueryable<Grade> GetBySpecializationId(long specId);
    }
}
