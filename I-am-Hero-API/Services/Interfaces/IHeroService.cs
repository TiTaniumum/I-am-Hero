using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface IHeroService
    {
        void SetResult(ActionResult<TokenDto> result);
        ActionResult<TokenDto>? GetResultOrDefault();
        void SetUser(User user);
        Task<IdDto> CreateHero(string heroName);
        Task<HeroDto> GetHero();
        Task EditHero(HeroDto dto);
        Task EditHeroName(string newHeroName);
        Task EditHeroLevelCalculationType(long levelCalculationTypeId);
        Task EditHeroExperience(long exp);
        Task<bool> IsHeroExist();
        Task<IdDto> CreateHeroAttribute(HeroAttributeDto dto);
        Task<HeroAttributesDto> GetHeroAttributes(long? id);
        Task EditHeroAttribute(HeroAttributeDto dto);
        Task DeleteHeroAttribute(long id);
        Task<IdDto> CreateHeroAttributeState(HeroAttributeStateDto dto);
        Task<IdsDto> CreateHeroAttributeStates(HeroAttributeStatesDto dto);
        Task<HeroAttributeStatesDto> GetHeroAttributeStates(long heroAttributeId);
        Task DeleteHeroAttributeState(long id);
        Task<IdDto> CreateHeroSkill(HeroSkillDto dto);
        Task<HeroSkillsDto> GetHeroSkills(long? id);
        Task EditHeroSkill(HeroSkillDto dto);
        Task DeleteHeroSkill(long id);
        Task<IdDto> CreateHeroStatusEffect(HeroStatusEffectDto dto);
        Task<HeroStatusEffectsDto> GetHeroStatusEffects(long? id);
        Task EditHeroStatusEffect(HeroStatusEffectDto dto);
        Task DeleteHeroStatusEffect(long id);
        Task<IdDto> CreateHeroBioPiece(HeroBioPieceDto dto);
        Task<HeroBioPiecesDto> GetHeroBioPieces(long? id);
        Task EditHeroBioPiece(HeroBioPieceDto dto);
        Task DeleteHeroBioPiece(long id);
        Task<IdDto> CreateHeroAchievement(HeroAchievementDto dto);
        Task<HeroAchievementsDto> GetHeroAchievements(long? id);
        Task EditHeroAchievement(HeroAchievementDto dto);
        Task DeleteHeroAchievement(long id);
        Task<IdDto> CreateQuest(QuestDto dto);
        Task<QuestsDto> GetQuests(long? id);
        Task EditQuest(QuestDto dto);
        Task DeleteQuest(long id);
        Task DeleteQuestBehaviour(long id);
    }
}