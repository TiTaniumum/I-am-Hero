using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroAchievementDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? cRarityId { get; set; }
        
        public HeroAchievementDto() { }
        public HeroAchievementDto(HeroAchievement achievement)
        {
            Id = achievement.Id;
            Name = achievement.Name;
            Description = achievement.Description;
            cRarityId = achievement.cRarityId;
        }
    }
}
