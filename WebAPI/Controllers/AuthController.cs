using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Zama.API.Data;
using BCrypt.Net;
using Zama.Models;
using Zama.API.Models.DTOs;
using Zama.API.Models.Responses;
using Zama.Data;
using Zama.API.Models;

namespace Zama.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ZamaDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ZamaDbContext context, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return Unauthorized("Email sau parolă invalidă");
                }

                var token = GenerateJwtToken(user);

                return Ok(new LoginResponse
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        Role = user.Role,
                        PhoneNumber = user.PhoneNumber,
                        LoyaltyPoints = user.LoyaltyPoints
                    },
                    Expiration = DateTime.UtcNow.AddHours(1)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la autentificare");
                return StatusCode(500, "A apărut o eroare la autentificare");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return BadRequest("Există deja un cont cu acest email");
                }

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    Role = "User",
                    RegistrationDate = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    user.Id,
                    user.Email,
                    user.Name,
                    user.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la înregistrare");
                return StatusCode(500, "A apărut o eroare la înregistrare");
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
