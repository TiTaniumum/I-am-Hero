namespace I_am_Hero_WPF.Models
{
    public class HeroSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        public int CLevelCalculationTypeId { get; set; }
    }
}
