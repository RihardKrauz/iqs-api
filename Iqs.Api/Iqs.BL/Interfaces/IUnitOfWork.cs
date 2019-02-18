using Iqs.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IGradesRepository Grades { get; }
        IUserGradesRepository UserGrades { get; }
        Task Save();
    }
}
