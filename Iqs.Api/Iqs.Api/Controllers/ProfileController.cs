using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.Api.Infrastructure;
using Iqs.Api.Models;
using Iqs.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        IUnitOfWork _uow;
        public ProfileController(IUnitOfWork uow) {
            _uow = uow;
        }

        [HttpGet("/user/{login}")]
        public async Task<MethodResult<UserDto>> GetUserData(string login) {
            try {
                var user = await _uow.Users.GetByLogin(login);

                if (user == null) {
                    return $"Cant find user by login: {login}".ToErrorMethodResult<UserDto>();
                }

                return new UserDto {
                    Name = user.Name,
                    Age = user.Age,
                    Role = user.Role
                }.ToSuccessMethodResult();
            }
            catch (Exception ex) {
                return $"Exception while getting users: {ex}".ToErrorMethodResult<UserDto>();
            }
        }
    }
}