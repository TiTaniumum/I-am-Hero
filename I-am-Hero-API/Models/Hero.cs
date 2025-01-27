namespace I_am_Hero_API.Models
{
    public class Hero
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public long Experience { get; set; }
        public long cLevelCalculationTypeId { get; set; }
        public cLevelCalculationType cLevelCalculationType { get; set; }
    }
}
