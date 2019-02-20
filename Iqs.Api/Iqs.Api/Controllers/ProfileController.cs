﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.DTO;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUsersEngine _usersEngine;

        public ProfileController(IUsersEngine usersEngine) {
            _usersEngine = usersEngine;
        }

        [HttpGet("/user/{login}")]
        [Authorize]
        public Task<MethodResult<EmployeeDto>> GetEmployeeData(string login) {
            return _usersEngine.GetEmployeeDataByLogin(login);
        }

        [HttpPost("/user")]
        public Task<MethodResult<SecuredUserDto>> CreateUser(UserAuthDto userGenerator)
        {
            return _usersEngine.CreateNewUser(userGenerator.User, userGenerator.Password);
        }
    }
}