namespace I_am_Hero_WPF.Models
{
    public class HeroAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CAttributeTypeId { get; set; }
        public int MinValue { get; set; }
        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int? CurrentStateId { get; set; }
    }
}
