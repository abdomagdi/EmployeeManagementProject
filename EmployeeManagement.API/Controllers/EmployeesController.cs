using AutoMapper;
using EmployeeManagement.Application.Departments;
using EmployeeManagement.Application.Dtos;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   

    public class EmployeesController : ControllerBase
    {
        private readonly IGenericService<ApplicationUser> _employeeService;
        private readonly IGenericService<Department> _departmentService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EmployeesController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IGenericService<ApplicationUser> employeeService,
            IGenericService<Department> departmentService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService= departmentService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ApplicationUser> employees = await _employeeService.GetAllAsync();
          // var
            //var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employees);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("Email already in use.");
            if (dto.Role is null)
                dto.Role = "Viewer";
            var roleExists = await _roleManager.RoleExistsAsync(dto.Role);
            if (!roleExists)
                return BadRequest("Specified role does not exist.");
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
                IsActive = dto.IsActive,
                DepartmentId= dto.DepartmentId
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, dto.Role);

            return Ok("User created successfully.");
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]      
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDto dto)
        {
          //  if (id != dto.Id) return BadRequest("ID mismatch");
            var employee = _mapper.Map<ApplicationUser>(dto);
            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPatch("{id}/activate")]        
        public async Task<IActionResult> Activate(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            employee.IsActive = true;
            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPatch("{id:guid}/deactivate")]      
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            employee.IsActive = false;
            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }
    }
}
