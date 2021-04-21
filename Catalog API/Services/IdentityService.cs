using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Models;
using Catalog_API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Catalog_API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var newUser = new IdentityUser()
            {
                Email = email,
                UserName = email
            };
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            var tokenDescription = new JwtSecurityToken(
                claims: new[]{
                    new Claim(ClaimTypes.Email, newUser.Email),
                    new Claim(ClaimTypes.Surname, newUser.Email),
                    new Claim("Id", newUser.Id),
                    new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                                                            SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);
            return new AuthenticationResult()
            {
                Success = true,
                Token = token
            };
        }
    }
}