using EmployeeManagement.Application.Dtos;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
       // private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthController(UserManager<ApplicationUser> userManager,  IConfiguration config)
        {
            _userManager = userManager;
          //  _roleManager = roleManager;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var startTime = DateTime.UtcNow;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            bool isValid = false;

            if (user != null)
            {
                isValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            }
            else
            {
                await _userManager.CheckPasswordAsync(new ApplicationUser(), dto.Password);
            }

            var elapsed = DateTime.UtcNow - startTime;
            var minimumDelay = TimeSpan.FromMilliseconds(1500);

            if (elapsed < minimumDelay)
            {
                await Task.Delay(minimumDelay - elapsed);
            }

            if (!isValid)
            {
                return Unauthorized("Invalid credentials");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return Ok(new { Token = token });
           
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
