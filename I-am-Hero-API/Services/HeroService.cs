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
        private IQueryable<HeroStatusEffect> userHeroStatusEffect { get => context.HeroStatusEffects.Where(x => x.UserId == user.Id); }
        private IQueryable<HeroBioPiece> userHeroBioPiece { get => context.HeroBioPieces.Where(x => x.UserId == user.Id); }
        private IQueryable<HeroAchievement> userHeroAchievement { get => context.HeroAchievements.Where(x => x.UserId == user.Id); }
        private IQueryable<Quest> userQuest { get => context.Quests.Where(x => x.UserId == user.Id); }
        private IQueryable<QuestBehaviour> userQuestBehaviour { get => context.QuestBehaviours.Where(x => x.UserId == user.Id); }
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
            return new IdDto { Id = newHero.Id };
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
            await context.HeroAttributes.Where(x => x.Id == id).ExecuteDeleteAsync();
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
            await context.HeroAttributeStates.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroAttributeState
        #region HeroSkill
        public async Task<IdDto> CreateHeroSkill(HeroSkillDto dto)
        {
            if (dto.Id == null)
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
            if (id != null)
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
        #region HeroStatusEffect
        public async Task<IdDto> CreateHeroStatusEffect(HeroStatusEffectDto dto)
        {
            if (dto.Name == null)
                throw new NullReferenceException("Name cannot be null when creating HeroStatusEffect");
            HeroStatusEffect effect = new HeroStatusEffect
            {
                UserId = user.Id,
                Name = dto.Name,
                Description = dto.Description,
                Value = dto.Value
            };
            await context.HeroStatusEffects.AddAsync(effect);
            await context.SaveChangesAsync();
            return new IdDto { Id = effect.Id };
        }

        public async Task<HeroStatusEffectsDto> GetHeroStatusEffects(long? id)
        {
            if (id != null)
                return new HeroStatusEffectsDto(await context.HeroStatusEffects.Where(x => x.Id == id).ToArrayAsync());
            return new HeroStatusEffectsDto(await userHeroStatusEffect.ToArrayAsync());
        }

        public async Task EditHeroStatusEffect(HeroStatusEffectDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing HeroStatusEffect");
            HeroStatusEffect? effect = await userHeroStatusEffect.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (effect == null)
                throw new NullReferenceException("HeroStatusEffect with this Id not found");
            if (dto.Name != null)
                effect.Name = dto.Name;
            if (dto.Description != null)
                effect.Description = dto.Description;
            if (dto.Value != null)
                effect.Value = dto.Value.Value;
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroStatusEffect(long id)
        {
            await context.HeroStatusEffects.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroStatusEffect
        #region HeroBioPiece
        public async Task<IdDto> CreateHeroBioPiece(HeroBioPieceDto dto)
        {
            if (dto.Text == null)
                throw new NullReferenceException("Text cannot be null when creating HeroBioPiece");
            HeroBioPiece piece = new HeroBioPiece { UserId = user.Id, Text = dto.Text };
            await context.HeroBioPieces.AddAsync(piece);
            await context.SaveChangesAsync();
            return new IdDto { Id = piece.Id };
        }

        public async Task<HeroBioPiecesDto> GetHeroBioPieces(long? id)
        {
            if (id != null)
                return new HeroBioPiecesDto(await context.HeroBioPieces.Where(x => x.Id == id).ToArrayAsync());
            return new HeroBioPiecesDto(await userHeroBioPiece.ToArrayAsync());
        }

        public async Task EditHeroBioPiece(HeroBioPieceDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing HeroBioPiece");
            HeroBioPiece? piece = await context.HeroBioPieces.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (piece == null)
                throw new NullReferenceException("HeroBioPiece with this Id not found");
            if (dto.Text != null)
                piece.Text = dto.Text;
            if (dto.CreateDate != null)
                piece.CreateDate = dto.CreateDate.Value;
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroBioPiece(long id)
        {
            await context.HeroBioPieces.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroBioPiece
        #region HeroAchievement
        public async Task<IdDto> CreateHeroAchievement(HeroAchievementDto dto)
        {
            if (dto.Name == null)
                throw new NullReferenceException("Name cannot be null when creating HeroAchievement");
            HeroAchievement achievement = new HeroAchievement
            {
                UserId = user.Id,
                Name = dto.Name,
                Description = dto.Description,
                cRarityId = dto.cRarityId
            };
            await context.HeroAchievements.AddAsync(achievement);
            await context.SaveChangesAsync();
            return new IdDto { Id = achievement.Id };
        }

        public async Task<HeroAchievementsDto> GetHeroAchievements(long? id)
        {
            if (id != null)
                return new HeroAchievementsDto(await context.HeroAchievements.Where(x => x.Id == id).ToArrayAsync());
            return new HeroAchievementsDto(await userHeroAchievement.ToArrayAsync());
        }

        public async Task EditHeroAchievement(HeroAchievementDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing HeroAchievement");
            HeroAchievement? achievement = await context.HeroAchievements.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (achievement == null)
                throw new NullReferenceException("HeroAchievement with this Id not found");
            if (dto.Name != null)
                achievement.Name = dto.Name;
            if (dto.Description != null)
                achievement.Description = dto.Description;
            if (dto.cRarityId != null)
                achievement.cRarityId = dto.cRarityId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroAchievement(long id)
        {
            await context.HeroAchievements.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion HeroAchievement
        #region Quest
        public async Task<IdDto> CreateQuest(QuestDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when creating Quest");
            if (dto.Title == null)
                throw new NullReferenceException("Title cannot be null when creating Quest");
            QuestBehaviourDto? completionQuestBehaviourDto = dto.CompletionQuestBehaviour;
            QuestBehaviour? completion = null;
            if (completionQuestBehaviourDto != null
                && (completionQuestBehaviourDto.HeroSkillId != null || completionQuestBehaviourDto.HeroAttirbuteId != null)
                && completionQuestBehaviourDto.Sign != null
                && completionQuestBehaviourDto.Value != null)
            {
                completion = new QuestBehaviour
                {
                    UserId = user.Id,
                    HeroAttirbuteId = completionQuestBehaviourDto.HeroAttirbuteId,
                    HeroSkillId = completionQuestBehaviourDto.HeroSkillId,
                    Sign = completionQuestBehaviourDto.Sign,
                    Value = completionQuestBehaviourDto.Value.Value
                };
            }
            QuestBehaviourDto? failureQuestBehaviourDto = dto.FailureQuestBehaviour;
            QuestBehaviour? failure = null;
            if (failureQuestBehaviourDto != null
                && (failureQuestBehaviourDto.HeroSkillId != null || failureQuestBehaviourDto.HeroAttirbuteId != null)
                && failureQuestBehaviourDto.Sign != null
                && failureQuestBehaviourDto.Value != null)
            {
                failure = new QuestBehaviour
                {
                    UserId = user.Id,
                    HeroAttirbuteId = failureQuestBehaviourDto.HeroAttirbuteId,
                    HeroSkillId = failureQuestBehaviourDto.HeroSkillId,
                    Sign = failureQuestBehaviourDto.Sign,
                    Value = failureQuestBehaviourDto.Value.Value
                };
            }
            Quest quest = new Quest
            {
                UserId = user.Id,
                Title = dto.Title,
                Description = dto.Description,
                Experinece = dto.Experinece ?? 0,
                CompletionQuestBehaviour = completion,
                FailureQuestBehaviour = failure,
                Priority = dto.Priority ?? 1,
                cDifficultyId = dto.cDifficultyId ?? 1,
                cQuestStatusId = dto.cQuestStatusId ?? 1,
                QuestLineId = dto.QuestLineId,
                Deadline = dto.Deadline,
                ArchiveDate = dto.ArchiveDate
            };
            await context.Quests.AddAsync(quest);
            await context.SaveChangesAsync();
            return new IdDto { Id = quest.Id };
        }

        public async Task<QuestsDto> GetQuests(long? id)
        {
            if (id != null)
                return new QuestsDto(
                    await context.Quests
                        .Include(x => x.CompletionQuestBehaviour)
                        .Include(x => x.FailureQuestBehaviour)
                        .Where(x => x.Id == id)
                        .ToArrayAsync()
                );
            return new QuestsDto(
                await userQuest
                    .Include(x => x.CompletionQuestBehaviour)
                    .Include(x => x.FailureQuestBehaviour)
                    .ToArrayAsync()
            );
        }

        public async Task EditQuest(QuestDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing Quest");
            Quest? quest = await userQuest
                .Include(x => x.CompletionQuestBehaviour)
                .Include(x => x.FailureQuestBehaviour)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (quest == null)
                throw new NullReferenceException("Quest with this Id not found");
            if (dto.Title != null)
                quest.Title = dto.Title;
            if (dto.Description != null)
                quest.Description = dto.Description;
            if (dto.Experinece != null)
                quest.Experinece = dto.Experinece.Value;
            if (dto.CompletionQuestBehaviour != null && dto.CompletionQuestBehaviour.Id != null)
            {
                QuestBehaviour? behaviour = quest.CompletionQuestBehaviour;
                if (behaviour != null)
                {
                    if (dto.CompletionQuestBehaviour.HeroAttirbuteId != null)
                        behaviour.HeroAttirbuteId = dto.CompletionQuestBehaviour.HeroAttirbuteId.Value;
                    if (dto.CompletionQuestBehaviour.HeroSkillId != null)
                        behaviour.HeroSkillId = dto.CompletionQuestBehaviour.HeroSkillId.Value;
                    if (dto.CompletionQuestBehaviour.Sign != null)
                        behaviour.Sign = dto.CompletionQuestBehaviour.Sign;
                    if (dto.CompletionQuestBehaviour.Value != null)
                        behaviour.Value = dto.CompletionQuestBehaviour.Value.Value;
                }
                else if ((dto.CompletionQuestBehaviour.HeroAttirbuteId != null || dto.CompletionQuestBehaviour.HeroSkillId != null)
                        && dto.CompletionQuestBehaviour.Sign != null
                        && dto.CompletionQuestBehaviour.Value != null)
                {
                    QuestBehaviour newBehaviour = new QuestBehaviour
                    {
                        UserId = user.Id,
                        HeroAttirbuteId = dto.CompletionQuestBehaviour.HeroAttirbuteId,
                        HeroSkillId = dto.CompletionQuestBehaviour.HeroSkillId,
                        Sign = dto.CompletionQuestBehaviour.Sign,
                        Value = dto.CompletionQuestBehaviour.Value.Value
                    };
                    quest.CompletionQuestBehaviour = newBehaviour;
                }
            }
            if (dto.FailureQuestBehaviour != null)
            {
                QuestBehaviour? behaviour = quest.FailureQuestBehaviour;
                if (behaviour != null)
                {
                    if (dto.FailureQuestBehaviour.HeroAttirbuteId != null)
                        behaviour.HeroAttirbuteId = dto.FailureQuestBehaviour.HeroAttirbuteId.Value;
                    if (dto.FailureQuestBehaviour.HeroSkillId != null)
                        behaviour.HeroSkillId = dto.FailureQuestBehaviour.HeroSkillId.Value;
                    if (dto.FailureQuestBehaviour.Sign != null)
                        behaviour.Sign = dto.FailureQuestBehaviour.Sign;
                    if (dto.FailureQuestBehaviour.Value != null)
                        behaviour.Value = dto.FailureQuestBehaviour.Value.Value;
                }
                else if ((dto.FailureQuestBehaviour.HeroAttirbuteId != null || dto.FailureQuestBehaviour.HeroSkillId != null)
                        && dto.FailureQuestBehaviour.Sign != null
                        && dto.FailureQuestBehaviour.Value != null)
                {
                    QuestBehaviour newBehaviour = new QuestBehaviour
                    {
                        UserId = user.Id,
                        HeroAttirbuteId = dto.FailureQuestBehaviour.HeroAttirbuteId,
                        HeroSkillId = dto.FailureQuestBehaviour.HeroSkillId,
                        Sign = dto.FailureQuestBehaviour.Sign,
                        Value = dto.FailureQuestBehaviour.Value.Value
                    };
                    quest.FailureQuestBehaviour = newBehaviour;
                }
            }
            if (dto.Priority != null)
                quest.Priority = dto.Priority.Value;
            if (dto.cDifficultyId != null)
                quest.cDifficultyId = dto.cDifficultyId.Value;
            if (dto.cQuestStatusId != null)
                quest.cQuestStatusId = dto.cQuestStatusId.Value;
            if (dto.QuestLineId != null)
                quest.QuestLineId = dto.QuestLineId;
            if (dto.Deadline != null)
                quest.Deadline = dto.Deadline;
            if (dto.ArchiveDate != null)
                quest.ArchiveDate = dto.ArchiveDate;
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuest(long id)
        {
            Quest? quest = await userQuest.Include(x => x.CompletionQuestBehaviour).Include(x => x.FailureQuestBehaviour).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (quest?.CompletionQuestBehaviour != null)
                await context.QuestBehaviours.Where(x => x.Id == quest.CompletionQuestBehaviour.Id).ExecuteDeleteAsync();
            if (quest?.FailureQuestBehaviour != null)
                await context.QuestBehaviours.Where(x => x.Id == quest.FailureQuestBehaviour.Id).ExecuteDeleteAsync();
            await context.Quests.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion Quest
        #region QuestBehaviour
        public async Task DeleteQuestBehaviour(long id)
        {
            await userQuestBehaviour.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion QuestBehaviour
        #region QuestLine
        public async Task<IdDto> CreateQuestLine(QuestLineDto dto)
        {
            if (dto.Title == null)
                throw new NullReferenceException("Title cannot be null when creating QuestLine");
            QuestBehaviourDto? completionQuestBehaviourDto = dto.CompletionQuestBehaviour;
            QuestBehaviour? completion = null;
            // TODO
        }
        #endregion QuestLine
    }
}
