using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class CalendarsDto : TokenDto
    {
        public IEnumerable<CalendarDto> Calendars { get; set; }

        public CalendarsDto()
        {
            Calendars = new List<CalendarDto>();
        }
        public CalendarsDto(IEnumerable<Calendar> calendars)
        {
            Calendars = calendars.Select(calendar => new CalendarDto(calendar));
        }
    }
}
