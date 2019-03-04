using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IGradesRepository : IGenericRepository<Grade>
    {
        Task<Specialization> GetBySpecializationId(long specId);
    }
}
