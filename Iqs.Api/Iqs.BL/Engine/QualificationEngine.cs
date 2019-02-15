using Iqs.BL.Interfaces;
using Iqs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Iqs.DTO;
using AutoMapper;
using Iqs.BL.Models;

namespace Iqs.BL.Engine
{
    public class QualificationEngine : IQualificationEngine
    {
        private readonly IUnitOfWork _uow;
        public QualificationEngine(IUnitOfWork uow) {
            _uow = uow;

            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
            });
        }

        public async Task<MethodResult<UserDto>> CreateNewUser(UserDto userDto, string password) {
            User user;
            try
            {
                user = Mapper.Map<User>(userDto);

                user.Role = "user";
                user.Salt = Cryptography.GenerateHash();
                user.RefreshToken = Cryptography.GenerateHash();
                user.PassHash = Convert.ToBase64String(Cryptography.GetHash(password + user.Salt));

                var createdUser = await _uow.Users.Create(user);
                await _uow.Save();

                return Mapper.Map<UserDto>(createdUser).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<UserDto>();
            }
            catch (Exception ex) {
                return ex.ToString().ToErrorMethodResult<UserDto>();
            }
        }

        public async Task<MethodResult<UserDto>> GetUserByLogin(string login) {
            try
            {
                var user = await _uow.Users.GetByLogin(login);
                return Mapper.Map<UserDto>(user).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<UserDto>();
            }
            catch (Exception ex)
            {
                return ex.ToString().ToErrorMethodResult<UserDto>();
            }
        }

    }
}
