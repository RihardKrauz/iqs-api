using Iqs.BL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IGradesRepository Grades { get; }
        IUserGradesRepository UserGrades { get; }
        Task Save();
    }
}
