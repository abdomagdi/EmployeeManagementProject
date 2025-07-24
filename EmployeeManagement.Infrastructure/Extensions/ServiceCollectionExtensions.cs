using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.IRepositories;
using EmployeeManagement.Infrastructure.Presistance;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<Department>), typeof(GenericRepository<Department>));
            services.AddScoped(typeof(IGenericRepository<ApplicationUser>), typeof(GenericRepository<ApplicationUser>));
            services.AddScoped(typeof(IGenericRepository<ApplicationRole>), typeof(GenericRepository<ApplicationRole>));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

           

        }
    }
}
