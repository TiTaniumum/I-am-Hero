using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroStatusEffectDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Value { get; set; }

        public HeroStatusEffectDto() { }
        public HeroStatusEffectDto(HeroStatusEffect heroStatusEffect)
        {
            Id = heroStatusEffect.Id;
            Name = heroStatusEffect.Name;
            Description = heroStatusEffect.Description;
            Value = heroStatusEffect.Value;
        }
    }
}
