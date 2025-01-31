using I_am_Hero_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using I_am_Hero_API.Models;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HeroController(ApplicationDbContext context) {
            _context = context;
        }

        // api/Hero/create
        //[Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateHero(long UserID, string HeroName) {

            User? user = await _context.Users.FindAsync(UserID);
            if (user == null)
            {
                return BadRequest("Such user does not exist!");
            }
            Hero newHero = new Hero { Name = HeroName };
            user.Hero = newHero;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
