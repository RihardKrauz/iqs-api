using AutoMapper.Configuration;
using Iqs.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using Iqs.DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Iqs.DAL.Interfaces;

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
    }
}
