using I_am_Hero_API.Data;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
        private IQueryable<Behaviour> userBehaviour { get => context.Behaviours.Where(x => x.UserId == user.Id); }
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
        #region PrivateMethods
        private Behaviour? CreateBehaviourObj(BehaviourDto? dto)
        {
            Behaviour? behaviourObj = null;
            if (dto != null
                && (dto.HeroSkillId != null || dto.HeroAttirbuteId != null)
                && dto.Sign != null
                && dto.Value != null)
            {
                behaviourObj = new Behaviour
                {
                    UserId = user.Id,
                    HeroAttributeId = dto.HeroAttirbuteId,
                    HeroSkillId = dto.HeroSkillId,
                    Sign = dto.Sign,
                    Value = dto.Value.Value
                };
            }
            return behaviourObj;
        }
        private bool EditBehaviourObj(Behaviour? behaviour, BehaviourDto? dto)
        {
            if (behaviour != null && dto != null)
            {
                if (dto.HeroAttirbuteId != null)
                    behaviour.HeroAttributeId = dto.HeroAttirbuteId.Value;
                if (dto.HeroSkillId != null)
                    behaviour.HeroSkillId = dto.HeroSkillId.Value;
                if (dto.Sign != null)
                    behaviour.Sign = dto.Sign;
                if (dto.Value != null)
                    behaviour.Value = dto.Value.Value;
                return true;
            }
            return false;
        }
        private Quest CreateQuestObj(QuestDto dto)
        {
            Behaviour? completion = CreateBehaviourObj(dto.CompletionBehaviour);
            Behaviour? failure = CreateBehaviourObj(dto.FailureBehaviour);
            Quest quest = new Quest
            {
                UserId = user.Id,
                Title = dto.Title ?? "",
                Description = dto.Description,
                Experinece = dto.Experinece ?? 0,
                CompletionBehaviour = completion,
                FailureBehaviour = failure,
                Priority = dto.Priority ?? 1,
                cDifficultyId = dto.cDifficultyId ?? 1,
                cQuestStatusId = dto.cQuestStatusId ?? 1,
                QuestLineId = dto.QuestLineId,
                Deadline = dto.Deadline,
                ArchiveDate = dto.ArchiveDate
            };
            return quest;
        }
        #endregion PrivateMethods
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
            if (dto.Title == null)
                throw new NullReferenceException("Title cannot be null when creating Quest");
            Quest quest = CreateQuestObj(dto);
            await context.Quests.AddAsync(quest);
            await context.SaveChangesAsync();
            return new IdDto { Id = quest.Id };
        }

        public async Task<QuestsDto> GetQuests(long? id)
        {
            if (id != null)
                return new QuestsDto(
                    await context.Quests
                        .Include(x => x.CompletionBehaviour)
                        .Include(x => x.FailureBehaviour)
                        .Where(x => x.Id == id)
                        .ToArrayAsync()
                );
            return new QuestsDto(
                await userQuest
                    .Include(x => x.CompletionBehaviour)
                    .Include(x => x.FailureBehaviour)
                    .ToArrayAsync()
            );
        }

        public async Task EditQuest(QuestDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing Quest");
            Quest? quest = await userQuest
                .Include(x => x.CompletionBehaviour)
                .Include(x => x.FailureBehaviour)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (quest == null)
                throw new NullReferenceException("Quest with this Id not found");
            if (dto.Title != null)
                quest.Title = dto.Title;
            if (dto.Description != null)
                quest.Description = dto.Description;
            if (dto.Experinece != null)
                quest.Experinece = dto.Experinece.Value;
            if (dto.CompletionBehaviour != null && !EditBehaviourObj(quest.CompletionBehaviour, dto.CompletionBehaviour))
                quest.CompletionBehaviour = CreateBehaviourObj(dto.CompletionBehaviour);
            if (dto.FailureBehaviour != null && !EditBehaviourObj(quest.FailureBehaviour, dto.FailureBehaviour))
                quest.FailureBehaviour = CreateBehaviourObj(dto.FailureBehaviour);
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
            Quest? quest = await userQuest
                .Include(x => x.CompletionBehaviour)
                .Include(x => x.FailureBehaviour)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (quest?.CompletionBehaviour != null)
                await context.Behaviours.Where(x => x.Id == quest.CompletionBehaviour.Id).ExecuteDeleteAsync();
            if (quest?.FailureBehaviour != null)
                await context.Behaviours.Where(x => x.Id == quest.FailureBehaviour.Id).ExecuteDeleteAsync();
            await context.Quests.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion Quest
        #region Behaviour
        public async Task DeleteBehaviour(long id)
        {
            await userBehaviour.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion Behaviour
        #region QuestLine
        public async Task<IdDto> CreateQuestLine(QuestLineDto dto)
        {
            if (dto.Title == null)
                throw new NullReferenceException("Title cannot be null when creating QuestLine");
            Behaviour? completion = CreateBehaviourObj(dto.CompletionBehaviour);
            Behaviour? failure = CreateBehaviourObj(dto.FailureBehaviour);
            IEnumerable<QuestDto> questDtos = dto.Quests;
            IEnumerable<Quest> quests = questDtos.Select(CreateQuestObj).ToList();
            QuestLine questLine = new QuestLine
            {
                UserId = user.Id,
                Title = dto.Title,
                Description = dto.Description,
                Experinece = dto.Experinece ?? 0,
                CompletionBehaviour = completion,
                FailureBehaviour = failure,
                Priority = dto.Priority ?? 1,
                cDifficultyId = dto.cDifficultyId ?? 1,
                cQuestStatusId = dto.cQuestStatusId ?? 1,
                Quests = quests,
                Deadline = dto.Deadline,
                ArchiveDate = dto.ArchiveDate
            };
            await context.QuestLines.AddAsync(questLine);
            await context.SaveChangesAsync();
            return new IdDto { Id = questLine.Id };
        }

        public async Task<QuestLinesDto> GetQuestLines(long? id)
        {
            if (id != null)
                return new QuestLinesDto(
                    await context.QuestLines
                        .Include(x => x.CompletionBehaviour)
                        .Include(x => x.FailureBehaviour)
                        .Include(x => x.Quests)
                        .Where(x => x.Id == id)
                        .ToArrayAsync()
                );
            return new QuestLinesDto(
                await context.QuestLines
                    .Include(x => x.CompletionBehaviour)
                    .Include(x => x.FailureBehaviour)
                    .Include(x => x.Quests)
                    .Where(x => x.UserId == user.Id)
                    .ToArrayAsync()
            );
        }

        public async Task EditQuestLine(QuestLineDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing QuestLine");
            QuestLine? questLine = await context.QuestLines
                .Include(x => x.CompletionBehaviour)
                .Include(x => x.FailureBehaviour)
                .Include(x => x.Quests)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (questLine == null)
                throw new NullReferenceException("QuestLine with this Id not found");
            if (dto.Title != null)
                questLine.Title = dto.Title;
            if (dto.Description != null)
                questLine.Description = dto.Description;
            if (dto.Experinece != null)
                questLine.Experinece = dto.Experinece.Value;
            if (dto.CompletionBehaviour != null && !EditBehaviourObj(questLine.CompletionBehaviour, dto.CompletionBehaviour))
                questLine.CompletionBehaviour = CreateBehaviourObj(dto.CompletionBehaviour);
            if (dto.FailureBehaviour != null && !EditBehaviourObj(questLine.FailureBehaviour, dto.FailureBehaviour))
                questLine.FailureBehaviour = CreateBehaviourObj(dto.FailureBehaviour);
            if (dto.Priority != null)
                questLine.Priority = dto.Priority.Value;
            if (dto.cDifficultyId != null)
                questLine.cDifficultyId = dto.cDifficultyId.Value;
            if (dto.cQuestStatusId != null)
                questLine.cQuestStatusId = dto.cQuestStatusId.Value;
            if (dto.Deadline != null)
                questLine.Deadline = dto.Deadline;
            if (dto.ArchiveDate != null)
                questLine.ArchiveDate = dto.ArchiveDate;
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuestLine(long id)
        {
            QuestLine? questLine = await context.QuestLines
                .Include(x => x.CompletionBehaviour)
                .Include(x => x.FailureBehaviour)
                .Include(x => x.Quests)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (questLine?.CompletionBehaviour != null)
                await context.Behaviours.Where(x => x.Id == questLine.CompletionBehaviour.Id).ExecuteDeleteAsync();
            if (questLine?.FailureBehaviour != null)
                await context.Behaviours.Where(x => x.Id == questLine.FailureBehaviour.Id).ExecuteDeleteAsync();
            if (questLine?.Quests != null)
                await context.Quests.Where(x => x.QuestLineId == id).ForEachAsync(async x => await DeleteQuest(x.Id));
            await context.QuestLines.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion QuestLine
        #region Calendar
        public async Task<IdDto> CreateCalendar(CalendarDto dto)
        {
            if (dto.Title == null)
                throw new NullReferenceException("Title cannot be null when creating Calendar");
            Behaviour? reward = CreateBehaviourObj(dto.RewardBehaviour);
            Behaviour? penalty = CreateBehaviourObj(dto.PenaltyBehaviour);
            Behaviour? ignore = CreateBehaviourObj(dto.IgnoreBehaviour);
            Calendar calendar = new Calendar
            {
                UserId = user.Id,
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                RewardBehaviour = reward,
                PenaltyBehaviour = penalty,
                IgnoreBehaviour = ignore,
                cCalendarBehaviourId = dto.cCalendarBehaviourId ?? 3 // Neutral
            };
            await context.Calendars.AddAsync(calendar);
            await context.SaveChangesAsync();
            return new IdDto { Id = calendar.Id };
        }

        public async Task<CalendarsDto> GetCalendars(long? id)
        {
            if (id != null)
                return new CalendarsDto(
                    await context.Calendars
                        .Where(x => x.Id == id)
                        .Include(x => x.RewardBehaviour)
                        .Include(x => x.PenaltyBehaviour)
                        .Include(x => x.IgnoreBehaviour)
                        .ToArrayAsync()
                );
            return new CalendarsDto(
                await context.Calendars
                    .Where(x => x.UserId == user.Id)
                    .Include(x => x.RewardBehaviour)
                    .Include(x => x.PenaltyBehaviour)
                    .Include(x => x.IgnoreBehaviour)
                    .ToArrayAsync()
            );
        }

        public async Task EditCalendar(CalendarDto dto)
        {
            if (dto.Id == null)
                throw new NullReferenceException("Id cannot be null when editing Calendar");
            Calendar? calendar = await context.Calendars
                .Include(x => x.RewardBehaviour)
                .Include(x => x.PenaltyBehaviour)
                .Include(x => x.IgnoreBehaviour)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (calendar == null)
                throw new NullReferenceException("Calendar with this Id not found");
            if (dto.Title != null)
                calendar.Title = dto.Title;
            if (dto.Description != null)
                calendar.Description = dto.Description;
            if (dto.StartDate != null)
                calendar.StartDate = dto.StartDate;
            if (dto.EndDate != null)
                calendar.EndDate = dto.EndDate;
            if (dto.RewardBehaviour != null && !EditBehaviourObj(calendar.RewardBehaviour, dto.RewardBehaviour))
                calendar.RewardBehaviour = CreateBehaviourObj(dto.RewardBehaviour);
            if (dto.PenaltyBehaviour != null && !EditBehaviourObj(calendar.PenaltyBehaviour, dto.PenaltyBehaviour))
                calendar.PenaltyBehaviour = CreateBehaviourObj(dto.PenaltyBehaviour);
            if (dto.IgnoreBehaviour != null && !EditBehaviourObj(calendar.IgnoreBehaviour, dto.IgnoreBehaviour))
                calendar.IgnoreBehaviour = CreateBehaviourObj(dto.IgnoreBehaviour);
            if (dto.cCalendarBehaviourId != null)
                calendar.cCalendarBehaviourId = dto.cCalendarBehaviourId.Value;
            await context.SaveChangesAsync();
        }

        public async Task DeleteCalendar(long id)
        {
            Calendar? calendar = await context.Calendars
                .Include(x => x.RewardBehaviour)
                .Include(x => x.PenaltyBehaviour)
                .Include(x => x.IgnoreBehaviour)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (calendar?.RewardBehaviour != null)
                await context.Behaviours.Where(x => x.Id == calendar.RewardBehaviour.Id).ExecuteDeleteAsync();
            if (calendar?.PenaltyBehaviour != null)
                await context.Behaviours.Where(x => x.Id == calendar.PenaltyBehaviour.Id).ExecuteDeleteAsync();
            if (calendar?.IgnoreBehaviour != null)
                await context.Behaviours.Where(x => x.Id == calendar.IgnoreBehaviour.Id).ExecuteDeleteAsync();
            await context.Calendars.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        #endregion Calendar
        #region CalendarAttendance
        public async Task<CalendarAttendanceDto> AttendCalendar(CalendarAttendanceDto dto)
        {
            if (dto.cCalendarStatusId == null)
                throw new NullReferenceException("cCalendarStatusId cannot be null when atteding in Calendar");
            if (dto.CalendarId == null && dto.Date == null && dto.Id == null
                || dto.CalendarId != null && dto.Date == null && dto.Id == null
                || dto.CalendarId == null && dto.Date != null && dto.Id == null)
                throw new NullReferenceException("CalendarId and Date or at least Id must be presend when attending in Calendar");
            CalendarAttendance? attendance = null;
            if (dto.Id != null)
                attendance = await context.CalendarAttendances.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (attendance == null && dto.CalendarId != null && dto.Date != null)
                attendance = await context.CalendarAttendances.FirstOrDefaultAsync(x => x.Date == dto.Date && x.CalendarId == dto.CalendarId);
            if (attendance != null)
                attendance.cCalendarStatusId = dto.cCalendarStatusId;
            if (attendance == null && dto.CalendarId != null && dto.Date != null)
                attendance = new CalendarAttendance
                {
                    CalendarId = dto.CalendarId.Value,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    cCalendarStatusId = dto.cCalendarStatusId
                };
            if (attendance == null)
                throw new NullReferenceException("CalendarId and Date cannot be null when making a new attendance in Calendar");

            await context.CalendarAttendances.AddAsync(attendance);
            await context.SaveChangesAsync();
            return new CalendarAttendanceDto(attendance);
        }
        public async Task<CalendarAttendancesDto> GetCalendarAttendances(long calendarId, DateOnly? dateFrom, DateOnly? dateTo)
        {
            if (dateFrom != null && dateTo != null)
                return new CalendarAttendancesDto(
                    await context.CalendarAttendances
                        .Where(x => x.CalendarId == calendarId && x.Date >= dateFrom && x.Date <= dateTo)
                        .ToArrayAsync()
                );
            if (dateFrom != null)
                return new CalendarAttendancesDto(
                    await context.CalendarAttendances
                        .Where(x => x.CalendarId == calendarId && x.Date >= dateFrom)
                        .ToArrayAsync()
                );
            if (dateTo != null)
                return new CalendarAttendancesDto(
                    await context.CalendarAttendances
                        .Where(x => x.CalendarId == calendarId && x.Date <= dateTo)
                        .ToArrayAsync()
                );
            return new CalendarAttendancesDto(
                await context.CalendarAttendances
                    .Where(x => x.CalendarId == calendarId)
                    .ToArrayAsync()
            );
        }
        #endregion CalendarAttendance
    }
}
