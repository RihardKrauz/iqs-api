﻿using Iqs.BL.Interfaces;
using Iqs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.DTO;
using AutoMapper;
using Iqs.DAL.Models;
using Iqs.DAL.Interfaces;
using System.Security.Claims;
using Iqs.BL.Engine.Security;

namespace Iqs.BL.Engine
{
    public class UsersEngine : IUsersEngine
    {
        private readonly IUnitOfWork _uow;
        public UsersEngine(IUnitOfWork uow) {
            _uow = uow;
        }

        public ClaimsIdentity GetIdentityForUser(User user)
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

        public async Task<string> GetRefreshToken(string userLogin)
        {
            var user = await _uow.Users.GetByLogin(userLogin);
            return user.RefreshToken;
        }

        public async Task<string> GenerateNewRefreshTokenForUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User is undefined");
            }
            user.RefreshToken = Cryptography.GenerateHash();

            _uow.Users.Update(user.Id, user);
            await _uow.Save();

            return user.RefreshToken;
        }

        public async Task<MethodResult<User>> GetUserByAuthData(string login, string pass)
        {
            try
            {
                User user = await _uow.Users.GetByLogin(login);
                if (user == null)
                {
                    return $"Cant specify user by Login = {login}".ToErrorMethodResult<User>();
                }

                bool isAuthenticated = Convert.ToBase64String(Cryptography.GetHash(pass + user.Salt)) == user.PassHash;
                return isAuthenticated ? user.ToSuccessMethodResult() : "Invalid password".ToErrorMethodResult<User>();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<User>();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<User>();
            }
        }

        public async Task<MethodResult<SecuredUserDto>> CreateNewUser(SecuredUserDto userDto, string password) {
            try
            {
                var getExistingUserResult = await GetUserByLogin(userDto.Login);
                if (getExistingUserResult.Value != null) {
                    return $"User with login '{userDto.Login}' already exists".ToErrorMethodResult<SecuredUserDto>();
                }

                var user = Mapper.Map<User>(userDto);
                var dep = await _uow.Departments.GetById(userDto.DepartmentId);
                var spec = await _uow.Specializations.GetById(userDto.SpecializationId);

                _uow.Departments.Attach(dep);
                _uow.Specializations.Attach(spec);

                user.Department = dep;
                user.Specialization = spec;
                user.Role = "user";
                user.Salt = Cryptography.GenerateHash();
                user.RefreshToken = Cryptography.GenerateHash();
                user.PassHash = Convert.ToBase64String(Cryptography.GetHash(password + user.Salt));

                var createdUser = await _uow.Users.Create(user);
                await _uow.Save();

                return Mapper.Map<SecuredUserDto>(createdUser).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<SecuredUserDto>();
            }
            catch (Exception ex) {
                return ex.ToErrorMethodResult<SecuredUserDto>();
            }
        }

        public async Task<MethodResult<EmployeeDto>> GetEmployeeDataByLogin(string login) {
            try
            {
                var user = await _uow.Users.GetByLogin(login);

                var grades = _uow.UserGrades.GetUserGradesForUser(user).ToList();

                var result = Mapper.Map<EmployeeDto>(user);
                result.Qualification = Mapper.Map<List<UserGradeDto>>(grades);

                return result.ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error: {ex}".ToErrorMethodResult<EmployeeDto>();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<EmployeeDto>();
            }
        }

        public async Task<MethodResult<SecuredUserDto>> GetUserByLogin(string login)
        {
            try
            {
                var user = await _uow.Users.GetByLogin(login);
                return Mapper.Map<SecuredUserDto>(user).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<SecuredUserDto>();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<SecuredUserDto>();
            }
        }

        public async Task<SecurityTokenDto> GenerateTokenForUser(User user) {

            ClaimsIdentity identity = GetIdentityForUser(user);

            return new SecurityTokenDto
            {
                access_token = TokenGenerator.GenerateTokenByClaims(identity.Claims),
                username = user.Login,
                refresh_token = await GenerateNewRefreshTokenForUser(user)
            };

        }

        public async Task<MethodResult<SecurityTokenDto>> AuthenticateUserAndGetSecurityToken(string login, string pass) {
            try
            {
                var getUserResult = await GetUserByAuthData(login, pass);
                if (!getUserResult.IsOk)
                    return getUserResult.GetExceptionResult<SecurityTokenDto, User>();

                var generatedSecurityData = await GenerateTokenForUser(getUserResult.Value);
                return generatedSecurityData.ToSuccessMethodResult();
            }
            catch (Exception ex) {
                return ex.ToErrorMethodResult<SecurityTokenDto>();
            }
        }

        public async Task<MethodResult<SecurityTokenDto>> RefreshTokenBySecurityData(SecurityTokenDto securityData) {
            try
            {
                var principalMethodResult = TokenGenerator.GetPrincipalFromExpiredToken(securityData.access_token);
                if (!principalMethodResult.IsOk || principalMethodResult.Value == null)
                {
                    return principalMethodResult.ExceptionMessage.ToErrorMethodResult<SecurityTokenDto>();
                }
                var principal = principalMethodResult.Value;
                var username = principal.Identity.Name;

                if (await GetRefreshToken(username) != securityData.refresh_token)
                {
                    return "Invalid refresh token".ToErrorMethodResult<SecurityTokenDto>();
                }

                var stableClaims = new List<string>() { ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType };

                var newJwtToken = TokenGenerator.GenerateTokenByClaims(principal
                    .Identities
                    .FirstOrDefault()?
                    .Claims?
                    .Where(c => stableClaims.Contains(c.Type)));

                var newRefreshToken = await GenerateNewRefreshTokenForUser(await _uow.Users.GetByLogin(username));

                var response = new SecurityTokenDto
                {
                    access_token = newJwtToken,
                    username = username,
                    refresh_token = newRefreshToken
                };

                return response.ToSuccessMethodResult();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<SecurityTokenDto>();
            }
        }

    }
}
