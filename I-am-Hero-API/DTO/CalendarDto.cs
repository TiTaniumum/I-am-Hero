using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class CalendarDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public BehaviourDto? RewardBehaviour { get; set; }
        public BehaviourDto? PenaltyBehaviour { get; set; }
        public BehaviourDto? IgnoreBehaviour { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? StopDate { get; set; }
        public long? cCalendarBehaviourId { get; set; }

        public CalendarDto(Calendar calendar)
        {
            Id = calendar.Id;
            Title = calendar.Title;
            Description = calendar.Description;
            StartDate = calendar.StartDate;
            EndDate = calendar.EndDate;
            if(calendar.RewardBehaviour != null)
                RewardBehaviour = new BehaviourDto(calendar.RewardBehaviour);
            if (calendar.PenaltyBehaviour != null)
                PenaltyBehaviour = new BehaviourDto(calendar.PenaltyBehaviour);
            if (calendar.IgnoreBehaviour != null)
                IgnoreBehaviour = new BehaviourDto(calendar.IgnoreBehaviour);
            CreateDate = calendar.CreateDate;
            StopDate = calendar.StopDate;
            cCalendarBehaviourId = calendar.cCalendarBehaviourId;
        }
    }
}
