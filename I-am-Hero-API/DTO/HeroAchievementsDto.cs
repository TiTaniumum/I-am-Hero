using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroAchievementsDto : TokenDto
    {
        public IEnumerable<HeroAchievementDto> Achievements { get; set; } = null!;

        public HeroAchievementsDto() { }
        public HeroAchievementsDto(IEnumerable<HeroAchievementDto> achievements)
        {
            Achievements = achievements;
        }
        public HeroAchievementsDto(IEnumerable<HeroAchievement> achievements)
        {
            Achievements = achievements.Select(x => new HeroAchievementDto(x));
        }
    }
}
