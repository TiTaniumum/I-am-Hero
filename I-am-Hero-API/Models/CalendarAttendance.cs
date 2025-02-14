namespace I_am_Hero_API.Models
{
    public class CalendarAttendance
    {
        public long Id { get; set; }
        public long CalendarId { get; set; }
        public Calendar Calendar { get; set; } = null!;
        public DateOnly Date { get; set; }
        public long? cCalendarStatusId { get; set; }
        public cCalendarStatus? cCalendarStatus { get; set; }
    }
}
