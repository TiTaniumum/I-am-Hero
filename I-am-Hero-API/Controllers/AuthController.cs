using I_am_Hero_API.DTO;
using I_am_Hero_API.Services.Interfaces;
using I_am_Hero_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Extensions;

namespace I_am_Hero_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ILoggerService loggerService;
        public AuthController(IAuthService authService, ILoggerService loggerService)
        {
            this.authService = authService;
            this.loggerService = loggerService;
        }
        // api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            try
            {
                User? user = await authService.RegisterUser(dto);
                if (user == null)
                {
                    Conflict(new
                    {
                        error = "UserAlreadyExists",
                        message = "A user with this email already exists. Please try logging in instead."
                    });
                }
                return Ok("Registered");
            }
            catch (Exception ex)
            {
#pragma warning disable CS4014 // Этот метод не нуждается в ожидании
                loggerService.LogException(null, HttpContext.Request.GetDisplayUrl(), ex);
#pragma warning restore CS4014 
                return BadRequest(ex.Message);
            }
        }
        // api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto)
        {
            try
            {
                Token? token = await authService.Login(dto);
                if (token == null)
                {
                    return BadRequest("Such user does not exist or wrong password");
                }
                return Ok(token.token);
            }
            catch (ArgumentOutOfRangeException ex)
            {
#pragma warning disable CS4014 // Этот метод не нуждается в ожидании
                loggerService.LogException(null, HttpContext.Request.GetDisplayUrl(), ex);
#pragma warning restore CS4014 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
#pragma warning disable CS4014 // Этот метод не нуждается в ожидании
                loggerService.LogException(null, HttpContext.Request.GetDisplayUrl(), ex);
#pragma warning restore CS4014 
                return BadRequest(ex.Message);
            }
        }
    }
}
