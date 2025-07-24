using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        //public string RoleId { get; set; }
        //public ApplicationRole Role { get; set; }
    }
}
