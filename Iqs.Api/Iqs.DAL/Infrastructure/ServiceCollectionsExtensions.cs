using AutoMapper.Configuration;
using Iqs.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using Iqs.DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Iqs.DAL.Interfaces;
using AutoMapper;
using Iqs.DTO;
using Iqs.DAL.Models;

namespace Iqs.BL.Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            services.RegisterDALServices(connectionString);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection RegisterMapperConfiguration(this IServiceCollection services) {

            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.CreateMap<SecuredUserDto, User>();
                cfg.CreateMap<User, SecuredUserDto>();
                cfg.CreateMap<EmployeeDto, User>();
                cfg.CreateMap<User, EmployeeDto>();
                cfg.CreateMap<GradeDto, Grade>();
                cfg.CreateMap<Grade, GradeDto>();
                cfg.CreateMap<DepartmentDto, Department>();
                cfg.CreateMap<Department, DepartmentDto>();
                cfg.CreateMap<List<Grade>, List<GradeDto>>();
                cfg.CreateMap<List<GradeDto>, List<Grade>>();
            });

            return services;
        }
    }
}
