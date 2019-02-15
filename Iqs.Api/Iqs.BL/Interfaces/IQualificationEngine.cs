using Iqs.DTO;
using Iqs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Interfaces
{
    public interface IQualificationEngine
    {
        Task<MethodResult<UserDto>> CreateNewUser(UserDto userDto, string password);
        Task<MethodResult<UserDto>> GetUserByLogin(string login);
    }
}
