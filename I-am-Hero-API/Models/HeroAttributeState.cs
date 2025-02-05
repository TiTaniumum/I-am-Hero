using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class HeroAttributeState
    {
        public long Id { get; set; }
        public long HeroAttributeId { get; set; }
        public HeroAttribute HeroAttribute { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
    }
}
