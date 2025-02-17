using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class QuestLinesDto : TokenDto
    {
        public IEnumerable<QuestLineDto> QuestLines { get; set; } = null!;
        public QuestLinesDto() { }
        public QuestLinesDto(IEnumerable<QuestLineDto> QuestLines)
        {
            this.QuestLines = QuestLines;
        }
        public QuestLinesDto(IEnumerable<QuestLine> questLines)
        {
            this.QuestLines = questLines.Select(x => new QuestLineDto(x)).ToList();
        }
    }
}
