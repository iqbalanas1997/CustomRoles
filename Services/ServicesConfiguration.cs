using CustomRoles.Data;
using CustomRoles.Infrastructure;
using CustomRoles.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomRoles.Services
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddDbContext<CustomRolesDbContext>();

            services.AddTransient<IActionRepo, ActionRepo>();
            services.AddTransient<IPagesRepo, PagesRepo>();
            services.AddTransient<IBusinessRolesRepo, BusinessRolesRepo>();
            services.AddTransient<IBusinessUserRepo, BusinessUserRepo>();
            services.AddTransient<IUserControlRepo, UserControlRepo>();

        }
    }
}
