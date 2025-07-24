using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Domain.Entities
{
    public class ApplicationRole :IdentityRole
    {
       // public string? RoleId { get; set; } // Must be nullable
        public string Permissions { get; set; } = "{}"; // Store permissions as JSON

    }
}