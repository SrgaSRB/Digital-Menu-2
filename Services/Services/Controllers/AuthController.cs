using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Data;
using Services.DTOs.AdminDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == dto.Username);

            if (admin == null || admin.PasswordHash != dto.Password) 
                return Unauthorized("Neispravni kredencijali");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim("UserId", admin.Id.ToString()),
                new Claim("IsGlobalAdmin", admin.Role == "superadmin" ? "true" : "false")
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new LoginResponseDto
            {
                Token = jwt,
                Username = admin.Username,
                Id = admin.Id,
                IsSuperAdmin = admin.Role == "superadmin"
            });
        }

    }
}
