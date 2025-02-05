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
        private IQueryable<HeroAttribute> userHeroAttribute { get => context.HeroAttributes.Where(x => x.UserId == user.Id); }
        public HeroService(ApplicationDbContext context)
        {
            this.context = context;
        }
        #region ServiceContext
        public void SetResult(ActionResult<TokenDto> result)
        {
            this.result = result;
        }
        public ActionResult<TokenDto>? GetResultOrDefault() => result;
        public void SetUser(User user)
        {
            this.user = user;
        }
        #endregion ServiceContext
        #region Hero
        public async Task CreateHero(string heroName)
        {
            Hero newHero = new Hero { Name = heroName };
            user.Hero = newHero;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<HeroDto> GetHero()
        {
            Hero hero = await userHero.FirstAsync();
            return new HeroDto(hero);
        }

        public async Task EditHero(HeroDto dto)
        {
            if (dto.cLevelCalculationTypeId != null && !await context.cLevelCalculationTypes.AnyAsync(x => x.Id == dto.cLevelCalculationTypeId))
                throw new ArgumentOutOfRangeException("cLevelCalculationTypeId is out of range");
            if (dto.Name != null)
                await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, dto.Name));
            if (dto.cLevelCalculationTypeId != null)
                await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.cLevelCalculationTypeId, dto.cLevelCalculationTypeId));
            if (dto.Experience != null)
                await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Experience, dto.Experience));
        }

        public async Task EditHeroName(string newHeroName)
        {
            await userHero.ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, newHeroName));
        }

        public async Task EditHeroLevelCalculationType(long levelCalculationTypeId)
        {
            if (!await context.cLevelCalculationTypes.AnyAsync(x => x.Id == levelCalculationTypeId))
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
        #endregion Hero
        #region HeroAttribute
        public async Task<IdDto> CreateHeroAttribute(HeroAttributeDto dto)
        {
            if (dto.Name == null)
                throw new NullReferenceException("The 'Name' field cannot be empty when creating a new hero attribute.");
            var heroAttribute = new HeroAttribute()
            {
                UserId = user.Id,
                Name = dto.Name,
                Description = dto.Description,
                cAttributeTypeId = dto.cAttributeTypeId ?? 1L,
                MinValue = dto.MinValue,
                Value = dto.Value,
                MaxValue = dto.MaxValue,
                CurrentStateId = null,
            };
            await context.HeroAttributes.AddAsync(heroAttribute);
            await context.SaveChangesAsync();
            return new IdDto { Id = heroAttribute.Id };
        }
        public async Task<HeroAttributesDto> GetHeroAttributes(long? id)
        {
            List<HeroAttributeDto> list = new List<HeroAttributeDto>();
            HeroAttribute[] arr;
            if (id != null)
                arr = await userHeroAttribute.Where(x => x.Id == id).ToArrayAsync();
            else
                arr = await userHeroAttribute.ToArrayAsync();
            foreach (HeroAttribute i in arr)
                list.Add(new HeroAttributeDto(i));
            return new HeroAttributesDto(list);
        }
        public async Task EditHeroAttribute(HeroAttributeDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing HeroAttribute");
            if (dto.Name != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, dto.Name));
            if (dto.Description != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Description, dto.Description));
            if (dto.cAttributeTypeId != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.cAttributeTypeId, dto.cAttributeTypeId));
            if (dto.MinValue != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.MinValue, dto.MinValue));
            if (dto.Value != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Value, dto.Value));
            if (dto.MaxValue != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.MaxValue, dto.MaxValue));
            if (dto.CurrentStateId != null)
                await userHeroAttribute
                    .Where(x => x.Id == dto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.CurrentStateId, dto.CurrentStateId));
        }
        #endregion HeroAttribute
    }
}
