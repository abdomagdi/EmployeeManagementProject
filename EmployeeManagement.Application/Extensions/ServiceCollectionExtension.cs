using EmployeeManagement.Application.Departments;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.IRepositories;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<Department>), typeof(GenericService<Department>));
            services.AddScoped(typeof(IGenericService<ApplicationUser>), typeof(GenericService<ApplicationUser>));
            services.AddScoped(typeof(IGenericService<ApplicationRole>), typeof(GenericService<ApplicationRole>));

            // services.AddScoped<IRestaurantsService, RestaurantsService>();
            services.AddAutoMapper(cfg => { }, typeof(ServiceCollectionExtension), typeof(ServiceCollectionExtension));
        }
    }
}
