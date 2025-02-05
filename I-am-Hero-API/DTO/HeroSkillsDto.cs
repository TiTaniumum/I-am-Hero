using I_am_Hero_API.Models;
namespace I_am_Hero_API.DTO
{
    public class HeroSkillsDto : TokenDto
    {
        public IEnumerable<HeroSkillDto> HeroSkills { get; set; } = null!;
        public HeroSkillsDto() { }
        public HeroSkillsDto(IEnumerable<HeroSkillDto> heroSkills)
        {
            HeroSkills = heroSkills;
        }
        public HeroSkillsDto(IEnumerable<HeroSkill> heroSkills)
        {
            HeroSkills = heroSkills.Select(x => new HeroSkillDto(x));
        }
    }
}
