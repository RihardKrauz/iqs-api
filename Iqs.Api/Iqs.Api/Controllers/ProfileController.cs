using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.DTO;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IQualificationEngine _qualificationEngine;

        public ProfileController(IQualificationEngine qualificationEngine) {
            _qualificationEngine = qualificationEngine;
        }

        [HttpGet("/user/{login}")]
        [Authorize]
        public async Task<MethodResult<UserDto>> GetUserData(string login) {
            return await _qualificationEngine.GetUserByLogin(login);
        }

        [HttpPost("/user")]
        public Task<MethodResult<UserDto>> CreateUser(UserGeneratorDto userGenerator)
        {
            return _qualificationEngine.CreateNewUser(userGenerator.User, userGenerator.Password);
        }
    }
}