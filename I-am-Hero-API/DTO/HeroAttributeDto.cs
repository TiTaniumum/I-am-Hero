using I_am_Hero_API.Models;
using Microsoft.IdentityModel.Tokens;

namespace I_am_Hero_API.DTO
{
    public class HeroAttributeDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? cAttributeTypeId { get; set; }
        public long? MinValue { get; set; }
        public long? Value { get; set; }
        public long? MaxValue { get; set; }
        public long? CurrentStateId { get; set; }

        public HeroAttributeDto() { }
        public HeroAttributeDto(HeroAttribute heroAttribute)
        {
            Id = heroAttribute.Id;
            Name = heroAttribute.Name;
            Description = heroAttribute.Description;
            cAttributeTypeId = heroAttribute.cAttributeTypeId;
            MinValue = heroAttribute.MinValue;
            Value = heroAttribute.Value;
            MaxValue = heroAttribute.MaxValue;
            CurrentStateId = heroAttribute.CurrentStateId;
        }
    }
}
