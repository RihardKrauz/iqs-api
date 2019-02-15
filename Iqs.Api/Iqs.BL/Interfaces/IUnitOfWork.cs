using Iqs.BL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UsersRepository Users { get; }
        GradesRepository Grades { get; }
        UserGradesRepository UserGrades { get; }
        Task Save();
    }
}
