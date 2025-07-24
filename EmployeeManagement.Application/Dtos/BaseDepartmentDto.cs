using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Dtos
{
    public record BaseDepartmentDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; init; } 
        [StringLength(100, MinimumLength = 2)]
        public string? Description { get; init; }
    }
}
