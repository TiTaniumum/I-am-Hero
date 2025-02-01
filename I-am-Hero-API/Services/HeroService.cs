using I_am_Hero_API.Data;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace I_am_Hero_API.Services
{
    public class HeroService : IHeroService
    {
        private readonly ApplicationDbContext context;

        private User user = null!;
        public HeroService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void SetUser(User user)
        {
            this.user = user;
        }
        public async Task CreateHero(string heroName)
        {
            Hero newHero = new Hero { Name = heroName };
            user.Hero = newHero;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<bool> IsHeroExist()
        {
            Hero? hero = await context.Heroes.FirstOrDefaultAsync(x => x.UserId == user.Id);
            return hero != null;
        }
    }
}
