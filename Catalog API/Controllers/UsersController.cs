using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Services;
using Catalog_API.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Catalog_API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("/register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] IdentityCreateDto user)
        {
            var authResult = await _identityService.RegisterAsync(user.Email, user.Password);

            if (!authResult.Success)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    Errors = authResult.Errors
                });
            }
            return Ok(new AuthSuccessResponse()
            {
                Token = authResult.Token
            });
        }

        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] IdentityCreateDto user)
        {
            var loginResult = await _identityService.LoginAsync(user.Email, user.Password);

            if (!loginResult.Success)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    Errors = loginResult.Errors
                });
            }
            return Ok(new AuthSuccessResponse()
            {
                Token = loginResult.Token
            });
        }
    }
}