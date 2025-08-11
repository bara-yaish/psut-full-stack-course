using HR.DTOs.Auth;
using HR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private HrDbContext _dbContext;
        public AuthController(HrDbContext dbContext) { _dbContext = dbContext; }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _dbContext.Users
                // ASK: is it a good practice? I thought usernames are supposed to be unique
                .FirstOrDefault(x => x.UserName.ToUpper() == loginDto.UserName.ToUpper());

            if (user is null)
            {
                return BadRequest("Invalid UserName or Password");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword))
            {
                return BadRequest("Invalid UserName or Password");
            }

            // User is now authenticated
            var token = GenerateJwtToken(user);

            return Ok(token);
        }

        [NonAction]
        private string GenerateJwtToken(User user)
        {
            // Best practice to use ClaimTypes rather than literally stating the Key like "Id"
            // to standardize claims
            var claims = new List<Claim> // Claims --> User Profile (what the user claims to be)
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            // -----------------------
            // Identify user's role  |
            // -----------------------
            if (user.IsAdmin) // If user is Admin
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else // If not, get user's role
            {
                var employee = _dbContext.Employees.Include(x => x.Lookup).FirstOrDefault(x => x.UserId == user.Id);
                claims.Add(new Claim(ClaimTypes.Role, employee.Lookup.Name));
            }

            // The secret key we used in the configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9z862Dl7slyWeLB8hmAzHbuoETkaWCE3gwiYbRPGZFY="));
            // Encrypt the key using the HS256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Combine all the above information into one token
            var tokenSettings = new JwtSecurityToken(
                    claims: claims, // User Profile
                    expires: DateTime.Now.AddDays(1), // Expiry date
                    signingCredentials: credentials // Encryption settings
                );

            // Now the token is completely configured, time to create the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenSettings);

            return token;
        }
    }
}
