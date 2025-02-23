namespace I_am_Hero_API.Models
{
    public class HeroHabbit
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public long? CheckinBehaviourId { get; set; }
        public Behaviour? CheckinBehaviour { get; set; }
        public long cPopupIntervalId { get; set; }
        public cPopupInterval cPopupInterval { get; set; } = null!;
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
