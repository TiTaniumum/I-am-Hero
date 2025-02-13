namespace I_am_Hero_API.Models
{
    public class Behaviour
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long? HeroAttributeId { get; set; }
        public HeroAttribute? HeroAttribute { get; set; }
        public long? HeroSkillId { get; set; }
        public HeroSkill? HeroSkill { get; set; }
        public string Sign { get; set; } = null!;
        public long Value { get; set; }
    }
}
