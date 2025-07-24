using AutoMapper;
using EmployeeManagement.Application.Departments;
using EmployeeManagement.Application.Dtos;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IGenericService<Department> _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IGenericService<Department> departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Department> departments = await _departmentService.GetAllAsync();
            IEnumerable<DepartmentDto> DepartmentDto= _mapper.Map<IEnumerable<DepartmentDto>>(departments);  
            return Ok(DepartmentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Department? department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound();

            DepartmentDto departmentDto =  _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartmentDto)
        {
           Department department = _mapper.Map<Department>(createDepartmentDto);
            await _departmentService.AddAsync(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto updateDepartment)
        {
            if (id != updateDepartment.Id) return BadRequest("ID mismatch");
            Department department = _mapper.Map<Department>(updateDepartment);
            await _departmentService.UpdateAsync(department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
