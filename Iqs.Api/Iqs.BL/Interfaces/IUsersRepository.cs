using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetByLogin(string login);
    }
}
