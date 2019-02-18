using Iqs.DAL.Contexts;
using Iqs.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Infrastructure
{
    public static class ServiceCollectionsDataExtensions
    {
        public static IServiceCollection RegisterDALServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
