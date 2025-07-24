using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Dtos
{
    public record class BaseEmployeeDto
    {
        
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }
    }
}
