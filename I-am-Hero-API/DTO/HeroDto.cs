using I_am_Hero_API.Models;
using Newtonsoft.Json.Serialization;
using NuGet.Packaging.Signing;

namespace I_am_Hero_API.DTO
{
    public class HeroDto : TokenDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? Experience { get; set; }
        public long? cLevelCalculationTypeId { get; set; }
        public HeroDto()
        {

        }
        public HeroDto(Hero hero)
        {
            Id = hero.Id;
            Name = hero.Name;
            Experience = hero.Experience;
            cLevelCalculationTypeId = hero.cLevelCalculationTypeId;
        }
    }
}
