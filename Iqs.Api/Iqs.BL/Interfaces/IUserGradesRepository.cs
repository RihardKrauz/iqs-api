using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IUserGradesRepository : IGenericRepository<UserGrade>
    {
        IEnumerable<Grade> GetGradesForUser(User user);
        Task<Grade> GetCurrentGradeForUser(User user);
    }
}
