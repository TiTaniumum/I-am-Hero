using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroStatusEffectsDto : TokenDto
    {
        public IEnumerable<HeroStatusEffectDto> HeroStatusEffects { get; set; } = null!;
        public HeroStatusEffectsDto() { }
        public HeroStatusEffectsDto(IEnumerable<HeroStatusEffectDto> heroStatusEffects)
        {
            HeroStatusEffects = heroStatusEffects;
        }
        public HeroStatusEffectsDto(IEnumerable<HeroStatusEffect> heroStatusEffects)
        {
            HeroStatusEffects = heroStatusEffects.Select(x => new HeroStatusEffectDto(x));
        }
    }
}
