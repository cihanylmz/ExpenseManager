using ExpenseManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        private readonly ApplicationDbContext _context;
        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] User user)
        {
            IActionResult response = Unauthorized();
            var curUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);


            if (user != null)
            {
                if (user.Password.Equals(curUser.Password))
                {
                    var issuer = configuration["Jwt:Issuer"];
                    var audience = configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature
                    );
                    
                    var subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, curUser.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Username),
                    });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = expires,
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(jwtToken);
                }
            }

            return (ActionResult)response;
        }
    }
}