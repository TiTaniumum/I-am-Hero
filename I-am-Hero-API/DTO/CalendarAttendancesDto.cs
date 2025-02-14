using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class CalendarAttendancesDto : TokenDto
    {
        public IEnumerable<CalendarAttendanceDto> CalendarAttendances { get; set; } = null!;

        public CalendarAttendancesDto() { }
        public CalendarAttendancesDto(IEnumerable<CalendarAttendance> calendarAttendances)
        {
            CalendarAttendances = calendarAttendances.Select(calendarAttendance => new CalendarAttendanceDto(calendarAttendance));
        }
    }
}
