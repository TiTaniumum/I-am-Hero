using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class HeroAchievement
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long? cRarityId { get; set; }
        public cRarity? cRarity { get; set; }
    }
}
