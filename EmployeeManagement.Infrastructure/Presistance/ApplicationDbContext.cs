using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Presistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        // public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN", Permissions = "{\"FullAccess\": true}" },
                new ApplicationRole { Id = "2", Name = "HR", NormalizedName = "HR", Permissions = "{\"CanManageEmployees\": true}" },
                new ApplicationRole { Id = "3", Name = "Viewer", NormalizedName = "VIEWER", Permissions = "{}" }
            );

           
        }
    }
}

