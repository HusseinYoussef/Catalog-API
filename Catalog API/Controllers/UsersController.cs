using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Catalog_API.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Catalog_API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public UsersController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [Route("auth")]
        [HttpGet]
        public IActionResult Authenticate()
        {
            var claims = new[]{
                new Claim(ClaimTypes.Name, "Catalog User"),
                new Claim(ClaimTypes.DateOfBirth, "4/16/2021"),
                new Claim(ClaimTypes.Role, "Normal User")
            };

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { success = true, token = jwt });
        }
    }
}