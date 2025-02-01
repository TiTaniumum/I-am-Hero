using I_am_Hero_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using I_am_Hero_API.Models;
using I_am_Hero_API.DTO;
using System.Security.Claims;
using I_am_Hero_API.Services.Interfaces;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthService authService;
        private readonly IHeroService heroService;
        public HeroController(ApplicationDbContext context, IAuthService authService, IHeroService heroService) {
            this.context = context;
            this.authService = authService;
            this.heroService = heroService;
        }

        // api/Hero/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateHero(string heroName) {
            if (!await HandleEndpointEnter())
                return BadRequest(new { Error = "User not found", Message = "Token had not existing user!" });

            await heroService.CreateHero(heroName);

            TokenDto tokenDto = new TokenDto();
            await HandleTokenRegeneration(tokenDto);
            return Ok(tokenDto);
        }

        private async Task<bool> HandleEndpointEnter()
        {
            User? user = await authService.GetUser(HttpContext);
            if (user == null)
            {
                return false;
            }
            heroService.SetUser(user);
            return true;
        }

        private async Task HandleTokenRegeneration(TokenDto dto)
        {
            Token? token = await authService.RegenerateToken(HttpContext);
            if (token == null)
                return;
            dto.Token = token.token;
            return;
        }
    }
}
