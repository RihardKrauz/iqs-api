using Iqs.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetByLogin(string login);
    }
}
