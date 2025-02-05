using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroAttributeStatesDto : TokenDto
    {
        public IEnumerable<HeroAttributeStateDto> HeroAttributeStates { get; set; } = null!;
        public HeroAttributeStatesDto() { }
        public HeroAttributeStatesDto(IEnumerable<HeroAttributeStateDto> heroAttributeStates)
        {
            HeroAttributeStates = heroAttributeStates;
        }
        public HeroAttributeStatesDto(List<HeroAttributeState> heroAttributeStates)
        {
            HeroAttributeStates = new List<HeroAttributeStateDto>();
            foreach (var heroAttributeState in heroAttributeStates)
            {
                ((List<HeroAttributeStateDto>)HeroAttributeStates).Add(new HeroAttributeStateDto(heroAttributeState));
            }
        }
    }
}
