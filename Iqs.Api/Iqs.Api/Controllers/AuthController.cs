using Iqs.DTO;
using Iqs.BL.Engine;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersEngine _usersEngine;

        public AuthController(IUsersEngine usersEngine) {
            _usersEngine = usersEngine;
        }

        [HttpPost("/token")]
        public Task<MethodResult<SecurityTokenDto>> AuthenticateUserAndGetSecurityToken([FromBody] AuthDto authData)
        {
            return _usersEngine.AuthenticateUserAndGetSecurityToken(authData.username, authData.password);
        }

        [HttpPut("/token")]
        public Task<MethodResult<SecurityTokenDto>> RefreshToken([FromBody] SecurityTokenDto securityData)
        {
            return _usersEngine.RefreshTokenBySecurityData(securityData);
        }

    }
}