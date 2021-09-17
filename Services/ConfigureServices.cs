using CustomRoles.Data;
using CustomRoles.Infrastructure;
using CustomRoles.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoles.Services
{
    public static class ConfigureCustomDbContext
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration, string ConnectionString)
        {
            services.AddDbContext<CustomRolesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConnectionString)));


            services.AddDbContext<CustomRolesDbContext>();

            services.AddTransient<IActionRepo, ActionRepo>();
            services.AddTransient<IPagesRepo, PagesRepo>();
            services.AddTransient<IBusinessRolesRepo, BusinessRolesRepo>();
            services.AddTransient<IBusinessUserRepo, BusinessUserRepo>();
            services.AddTransient<IUserControlRepo, UserControlRepo>();


        }
    }
}
