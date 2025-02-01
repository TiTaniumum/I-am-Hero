﻿using I_am_Hero_API.DTO;
using I_am_Hero_API.Services.Interfaces;
using I_am_Hero_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace I_am_Hero_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto)
        {
            Token? token = await authService.Login(dto);
            if (token == null)
            {
                return BadRequest("Such user does not exist or wrong password");
            }
            return Ok(token.token);
        }
    }
}
