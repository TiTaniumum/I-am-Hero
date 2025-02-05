using I_am_Hero_API.Data;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace I_am_Hero_API.Services
{
    public class HeroService : IHeroService
    {
        private readonly ApplicationDbContext context;

        private User user = null!;
        private ActionResult<TokenDto>? result;
        private IQueryable<Hero> userHero { get => context.Heroes.Where(x => x.UserId == user.Id); }
        private IQueryable<HeroAttribute> userHeroAttribute { get => context.HeroAttributes.Where(x => x.UserId == user.Id); }
        private IQueryable<HeroSkill> userHeroSkill { get => context.HeroSkills.Where(x => x.UserId == user.Id); }
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
        public async Task<IdDto> CreateHero(string heroName)
        {
            Hero newHero = new Hero { Name = heroName };
            user.Hero = newHero;
            await context.SaveChangesAsync();
            return new IdDto { Id = newHero.Id};
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
            Hero? hero = await userHero.FirstOrDefaultAsync();
            if (hero == null)
                throw new NullReferenceException("Hero not found");
            if (dto.Name != null)
                hero.Name = dto.Name;
            if (dto.cLevelCalculationTypeId != null)
                hero.cLevelCalculationTypeId = dto.cLevelCalculationTypeId.Value;
            if (dto.Experience != null)
                hero.Experience = dto.Experience.Value;
            await context.SaveChangesAsync();
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
            HeroAttribute? attribute = await userHeroAttribute.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (attribute == null)
                throw new NullReferenceException("HeroAttribute with this Id not found");
            if (dto.Name != null)
                attribute.Name = dto.Name;
            if (dto.Description != null)
                attribute.Description = dto.Description;
            if (dto.cAttributeTypeId != null)
                attribute.cAttributeTypeId = dto.cAttributeTypeId.Value;
            if (dto.MinValue != null)
                attribute.MinValue = dto.MinValue;
            if (dto.Value != null)
                attribute.Value = dto.Value;
            if (dto.MaxValue != null)
                attribute.MaxValue = dto.MaxValue;
            if (dto.CurrentStateId != null)
                attribute.CurrentStateId = dto.CurrentStateId;
            await context.SaveChangesAsync();
        }
        public async Task DeleteHeroAttribute(long id)
        {
            await context.HeroAttributes.Where(x=>x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroAttribute
        #region HeroAttributeState
        public async Task<IdDto> CreateHeroAttributeState(HeroAttributeStateDto dto)
        {
            HeroAttributeState state = new HeroAttributeState
            {
                HeroAttributeId = dto.HeroAttributeId,
                Name = dto.Name,
            };
            await context.HeroAttributeStates.AddAsync(state);
            await context.SaveChangesAsync();
            return new IdDto { Id = state.Id };
        }
        public async Task<IdsDto> CreateHeroAttributeStates(HeroAttributeStatesDto dto)
        {
            List<HeroAttributeState> list = new List<HeroAttributeState>();
            foreach (HeroAttributeStateDto i in dto.HeroAttributeStates)
            {
                HeroAttributeState state = new HeroAttributeState
                {
                    HeroAttributeId = i.HeroAttributeId,
                    Name = i.Name,
                };
                list.Add(state);
            }
            await context.HeroAttributeStates.AddRangeAsync(list);
            await context.SaveChangesAsync();
            return new IdsDto { Ids = list.Select(x => x.Id).ToArray() };
        }
        public async Task<HeroAttributeStatesDto> GetHeroAttributeStates(long heroAttributeId)
        {
            List<HeroAttributeState> list = await context.HeroAttributeStates.Where(x => x.HeroAttributeId == heroAttributeId).ToListAsync();
            return new HeroAttributeStatesDto(list);
        }
        public async Task DeleteHeroAttributeState(long id)
        {
            await context.HeroAttributeStates.Where(x=>x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroAttributeState
        #region HeroSkill
        public async Task<IdDto> CreateHeroSkill(HeroSkillDto dto)
        {
            if(dto.Id == null)
                throw new NullReferenceException("Id cannot be null when creating HeroSkill");
            if (dto.Name == null)
                throw new NullReferenceException("Name cannot be null when creating HeroSkill");
            HeroSkill skill = new HeroSkill
            {
                UserId = user.Id,
                Name = dto.Name,
                Description = dto.Description,
                Experience = dto.Experience ?? 0,
                cLevelCalculationTypeId = 1
            };
            await context.HeroSkills.AddAsync(skill);
            await context.SaveChangesAsync();
            return new IdDto { Id = skill.Id };
        }

        public async Task<HeroSkillsDto> GetHeroSkills(long? id)
        {
            if(id != null)
                return new HeroSkillsDto(await context.HeroSkills.Where(x => x.Id == id).ToArrayAsync());
            return new HeroSkillsDto(await userHeroSkill.ToArrayAsync());
        }

        public async Task EditHeroSkill(HeroSkillDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing HeroSkill");
            HeroSkill? skill = await userHeroSkill.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (skill == null)
                throw new NullReferenceException("HeroSkill with this Id not found");
            if (dto.Name != null)
                skill.Name = dto.Name;
            if (dto.Description != null)
                skill.Description = dto.Description;
            if (dto.Experience != null)
                skill.Experience = dto.Experience.Value;
            if (dto.cLevelCalculationTypeId != null)
                skill.cLevelCalculationTypeId = dto.cLevelCalculationTypeId.Value;
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroSkill(long id)
        {
            await context.HeroSkills.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroSkill
    }
}
