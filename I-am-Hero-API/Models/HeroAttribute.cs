namespace I_am_Hero_API.Models
{
    public class HeroAttribute
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long cAttributeTypeId { get; set; }
        public cAttributeType cAttributeType { get; set; } = null!;
        public long? MinValue { get; set; }
        public long? Value { get; set; }
        public long? MaxValue { get; set; }
        public long? CurrentStateId { get; set; }
        public IEnumerable<HeroAttributeState> HeroAttributeStates { get; set; } = new List<HeroAttributeState>();
    }
}
