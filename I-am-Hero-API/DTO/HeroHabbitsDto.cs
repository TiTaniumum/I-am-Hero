using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroHabbitsDto : TokenDto
    {
        public IEnumerable<HeroHabbitDto> HeroHabbits { get; set; } = null!;
        public HeroHabbitsDto() { }
        public HeroHabbitsDto(IEnumerable<HeroHabbitDto> heroHabbits)
        {
            HeroHabbits = heroHabbits;
        }
        public HeroHabbitsDto(IEnumerable<HeroHabbit> heroHabbits)
        {
            HeroHabbits = heroHabbits.Select(x => new HeroHabbitDto(x));
        }
    }
}
