using Microsoft.EntityFrameworkCore;

namespace I_am_Hero_API.Models
{
    public class Calendar
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Comment("Дата начала действия календаря")]
        public DateTime? StartDate { get; set; }
        [Comment("Дата окончания действия календаря")]
        public DateTime? EndDate { get; set; }
        public long? RewardBehaviourId { get; set; }
        public Behaviour? RewardBehaviour { get; set; }
        public long? PenaltyBehaviourId { get; set; }
        public Behaviour? PenaltyBehaviour { get; set; }
        public long? IgnoreBehaviourId { get; set; }
        public Behaviour? IgnoreBehaviour { get; set; }
        [Comment("Дата создания календаря. По умолчанию выставляется GETDATE()")]
        public DateTime CreateDate { get; set; }
        [Comment("Дата прекращения работы календаря. Пользователь не сможет ставть attendance. Выставляется когда пользователь больше не хочет пользоваться календарем.")]
        public DateTime? StopDate { get; set; }
        public long cCalendarBehaviourId { get; set; }
        public cCalendarBehaviour cCalendarBehaviour { get; set; } = null!;
        public IEnumerable<CalendarAttendance> CalendarAttendances { get; set; } = new List<CalendarAttendance>();
    }
}
