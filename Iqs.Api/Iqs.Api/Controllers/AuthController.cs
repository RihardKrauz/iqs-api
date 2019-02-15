using Iqs.DTO;
using Iqs.Api.Security;
using Iqs.BL.Engine;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Iqs.BL.Models;
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
        private readonly IUnitOfWork _uow;

        private async Task<MethodResult<User>> GetUserByAuthData(string login, string pass)
        {
            User user = await _uow.Users.GetByLogin(login);
            if (user == null)
            {
                return $"Cant specify user by Login = {login}".ToErrorMethodResult<User>();
            }

            bool isAuthenticated = Convert.ToBase64String(Cryptography.GetHash(pass + user.Salt)) == user.PassHash;
            return isAuthenticated ? user.ToSuccessMethodResult() : "Invalid password".ToErrorMethodResult<User>();
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
            {
                return null;
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            return new ClaimsIdentity(claims, "Token", user.Login, user.Role);
        }

        private async Task<string> SaveRefreshToken(User user)
        {
            if (user == null) {
                throw new Exception("User is undefined");
            }
            user.RefreshToken = Cryptography.GenerateHash();

            _uow.Users.Update(user.Id, user);
            await _uow.Save();

            return user.RefreshToken;
        }

        private async Task<string> GetRefreshToken(string userLogin)
        {
            var user = await _uow.Users.GetByLogin(userLogin);
            return user.RefreshToken;
        }

        public AuthController(IUnitOfWork uow) {
            _uow = uow;
        }

        [HttpPost("/token")]
        public async Task<MethodResult<SecurityTokenDto>> SetToken([FromBody] AuthDataDto authData)
        {
            try
            {
                MethodResult<User> getUserMethodResult = await GetUserByAuthData(authData.username, authData.password);

                if (!getUserMethodResult.IsOk)
                {
                    return getUserMethodResult.ExceptionMessage.ToErrorMethodResult<SecurityTokenDto>();
                }
                else
                {
                    ClaimsIdentity identity = GetIdentity(getUserMethodResult.Value);

                    var response = new SecurityTokenDto
                    {
                        access_token = TokenGenerator.GenerateTokenByClaims(identity.Claims),
                        username = getUserMethodResult.Value.Login,
                        refresh_token = await SaveRefreshToken(getUserMethodResult.Value)
                    };

                    // return  JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }).ToSuccessMethodResult();
                    return response.ToSuccessMethodResult();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString().ToErrorMethodResult<SecurityTokenDto>();
            }
        }

        [HttpPut("/token")]
        public async Task<MethodResult<SecurityTokenDto>> RefreshToken([FromBody] SecurityTokenDto securityData)
        {
            try
            {
                var principalMethodResult = TokenGenerator.GetPrincipalFromExpiredToken(securityData.access_token);
                if (!principalMethodResult.IsOk || principalMethodResult.Value == null) {
                    return principalMethodResult.ExceptionMessage.ToErrorMethodResult<SecurityTokenDto>();
                }
                var principal = principalMethodResult.Value;
                var username = principal.Identity.Name;

                if (await GetRefreshToken(username) != securityData.refresh_token) {
                    return "Invalid refresh token".ToErrorMethodResult<SecurityTokenDto>();
                }

                var stableClaims = new List<string>() { ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType };

                var newJwtToken = TokenGenerator.GenerateTokenByClaims(principal.Identities.FirstOrDefault()?.Claims?.Where(c => stableClaims.Contains(c.Type)));
                var newRefreshToken = await SaveRefreshToken(await _uow.Users.GetByLogin(username));

                var response = new SecurityTokenDto
                {
                    access_token = newJwtToken,
                    username = username,
                    refresh_token = newRefreshToken
                };

                // return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }).ToSuccessMethodResult();
                return response.ToSuccessMethodResult();
            }
            catch (Exception ex)
            {
                return ex.Message.ToErrorMethodResult<SecurityTokenDto>();
            }
        }

    }
}