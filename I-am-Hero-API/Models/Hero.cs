using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class Hero
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public long Experience { get; set; } = 0;
        public long cLevelCalculationTypeId { get; set; }
        public cLevelCalculationType cLevelCalculationType { get; set; } = null!;
    }
}
