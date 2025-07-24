using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Dtos
{
    public record UpdateEmployeeDto:BaseEmployeeDto
    {
        public int Id { get; set; }
    }
}
