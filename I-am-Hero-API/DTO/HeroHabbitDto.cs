using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroHabbitDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public BehaviourDto? CheckinBehaviourDto { get; set; }
        public long? cPopupIntervalId { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }

        public HeroHabbitDto() { }
        public HeroHabbitDto(HeroHabbit heroHabbit)
        {
            Id = heroHabbit.Id;
            Title = heroHabbit.Title;
            Description = heroHabbit.Description;
            if(heroHabbit.CheckinBehaviour != null)
                CheckinBehaviourDto = new BehaviourDto(heroHabbit.CheckinBehaviour);
            cPopupIntervalId = heroHabbit.cPopupIntervalId;
            LastUpdateDateTime = heroHabbit.LastUpdateDateTime;
        }
    }
}
