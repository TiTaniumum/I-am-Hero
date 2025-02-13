namespace I_am_Hero_API.Models
{
    public class Calendar
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? RewardBehaviourId { get; set; }
        public Behaviour? RewardBehaviour { get; set; }
        public long? PenaltyBehaviourId { get; set; }
        public Behaviour? PenaltyBehaviour { get; set; }
        public long? IgnoreBehaviourId { get; set; }
        public Behaviour? IgnoreBehaviour { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? StopDate { get; set; }
        public long cCalendarBehaviourId { get; set; }
        public cCalendarBehaviour cCalendarBehaviour { get; set; } = null!;
        public IEnumerable<CalendarAttendance> CalendarAttendances { get; set; } = new List<CalendarAttendance>();
    }
}
