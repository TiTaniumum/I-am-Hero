using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class BehaviourDto
    {
        public long? Id { get; set; }
        public long? HeroAttirbuteId { get; set; }
        public long? HeroSkillId { get; set; }
        public string? Sign { get; set; }
        public long? Value { get; set; }
        
        public BehaviourDto() { }

        public BehaviourDto(Behaviour behaviour)
        {
            Id = behaviour.Id;
            HeroAttirbuteId = behaviour.HeroAttributeId;
            HeroSkillId = behaviour.HeroSkillId;
            Sign = behaviour.Sign;
            Value = behaviour.Value;
        }
    }
}
