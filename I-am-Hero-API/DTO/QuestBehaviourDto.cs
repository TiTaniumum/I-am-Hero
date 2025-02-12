using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class QuestBehaviourDto
    {
        public long? Id { get; set; }
        public long? HeroAttirbuteId { get; set; }
        public long? HeroSkillId { get; set; }
        public string? Sign { get; set; }
        public long? Value { get; set; }
        
        public QuestBehaviourDto() { }

        public QuestBehaviourDto(QuestBehaviour questBehaviour)
        {
            Id = questBehaviour.Id;
            HeroAttirbuteId = questBehaviour.HeroAttributeId;
            HeroSkillId = questBehaviour.HeroSkillId;
            Sign = questBehaviour.Sign;
            Value = questBehaviour.Value;
        }
    }
}
