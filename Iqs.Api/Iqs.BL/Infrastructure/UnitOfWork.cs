using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseContext _dbContext;

        private IUsersRepository usersRepository;
        private IGradesRepository gradesRepository;
        private IUserGradesRepository userGradesRepository;
        private IDepartmentsRepository departmentsRepository;

        private bool disposed = false;

        public UnitOfWork(BaseContext dbContext) {
            _dbContext = dbContext;
        }

        public IUsersRepository Users {
            get {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(_dbContext);
                return usersRepository;
            }
        }

        public IDepartmentsRepository Departments
        {
            get
            {
                if (departmentsRepository == null)
                    departmentsRepository = new DepartmentsRepository(_dbContext);
                return departmentsRepository;
            }
        }

        public IGradesRepository Grades
        {
            get
            {
                if (gradesRepository == null)
                    gradesRepository = new GradesRepository(_dbContext);
                return gradesRepository;
            }
        }

        public IUserGradesRepository UserGrades
        {
            get
            {
                if (userGradesRepository == null)
                    userGradesRepository = new UserGradesRepository(_dbContext);
                return userGradesRepository;
            }
        }

        public async Task Save() {
            await _dbContext.SaveChangesAsync();
        }

        

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
