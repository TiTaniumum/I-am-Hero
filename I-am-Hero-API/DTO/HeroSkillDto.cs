using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroSkillDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Experience { get; set; }
        public long? cLevelCalculationTypeId { get; set; }

        public HeroSkillDto() { }
        public HeroSkillDto(HeroSkill heroSkill)
        {
            Id = heroSkill.Id;
            Name = heroSkill.Name;
            Description = heroSkill.Description;
            Experience = heroSkill.Experience;
            cLevelCalculationTypeId = heroSkill.cLevelCalculationTypeId;
        }
    }
}
