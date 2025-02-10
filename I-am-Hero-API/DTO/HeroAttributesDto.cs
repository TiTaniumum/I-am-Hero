namespace I_am_Hero_API.DTO
{
    public class HeroAttributesDto : TokenDto
    {
        public IEnumerable<HeroAttributeDto> heroAttributes { get; set; } = null!;
        public HeroAttributesDto() { }
        public HeroAttributesDto(IEnumerable<HeroAttributeDto> heroAttributes)
        {
            this.heroAttributes = heroAttributes;
        }
    }
}
