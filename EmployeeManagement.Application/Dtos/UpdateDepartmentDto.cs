using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Dtos
{
    public record UpdateDepartmentDto: BaseDepartmentDto
    {
        public int Id { get; set; }
    }
}
