using I_am_Hero_API.Data;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace I_am_Hero_API.Services
{
    public class HeroService : IHeroService
    {
        private readonly ApplicationDbContext context;

        private User user = null!;
        private ActionResult<TokenDto>? result;
        private IQueryable<Hero> userHero { get => context.Heroes.Where(x => x.UserId == user.Id); }
        public HeroService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void SetResult(ActionResult<TokenDto> result)
        {
            this.result = result;
        }
        public ActionResult<TokenDto>? GetResultOrDefault() => result;
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

        public async Task EditHeroName(string newHeroName)
        {
            await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, newHeroName));
        }

        public async Task EditHeroLevelCalculationType(long levelCalculationTypeId)
        {
            if(!await context.cLevelCalculationTypes.AnyAsync(x=>x.Id == levelCalculationTypeId))
                throw new ArgumentOutOfRangeException("cLevelCalculationTypeId is out of range");
            await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.cLevelCalculationTypeId, levelCalculationTypeId));
        }
        public async Task EditHeroExperience(long exp)
        {
            await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Experience, exp));
        }
        public async Task<bool> IsHeroExist()
        {
            Hero? hero = await context.Heroes.FirstOrDefaultAsync(x => x.UserId == user.Id);
            return hero != null;
        }
    }
}
