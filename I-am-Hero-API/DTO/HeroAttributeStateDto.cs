using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroAttributeStateDto : TokenDto
    {
        public long? Id { get; set; }
        public long HeroAttributeId { get; set; }
        public string Name { get; set; } = null!;

        public HeroAttributeStateDto() { }
        public HeroAttributeStateDto(HeroAttributeState heroAttributeState)
        {
            Id = heroAttributeState.Id;
            HeroAttributeId = heroAttributeState.HeroAttributeId;
            Name = heroAttributeState.Name;
        }
    }
}
