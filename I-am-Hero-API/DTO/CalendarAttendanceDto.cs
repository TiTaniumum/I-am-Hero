using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class CalendarAttendanceDto : TokenDto
    {
        public long? Id { get; set; }
        public long? CalendarId { get; set; }
        public DateOnly? Date { get; set; }
        public long? cCalendarStatusId { get; set; }

        public CalendarAttendanceDto() { }
        public CalendarAttendanceDto(CalendarAttendance calendarAttendance)
        {
            Id = calendarAttendance.Id;
            CalendarId = calendarAttendance.CalendarId;
            Date = calendarAttendance.Date;
            cCalendarStatusId = calendarAttendance.cCalendarStatusId;
        }
    }
}
