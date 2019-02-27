using Iqs.DTO;
using Iqs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Interfaces
{
    public interface IUsersEngine
    {
        Task<MethodResult<SecuredUserDto>> CreateNewUser(SecuredUserDto userDto, string password);
        Task<MethodResult<EmployeeDto>> GetEmployeeDataByLogin(string login);
        Task<MethodResult<SecuredUserDto>> GetUserByLogin(string login);
        Task<MethodResult<SecurityTokenDto>> AuthenticateUserAndGetSecurityToken(string login, string pass);
        Task<MethodResult<SecurityTokenDto>> RefreshTokenBySecurityData(SecurityTokenDto securityData);
    }
}
