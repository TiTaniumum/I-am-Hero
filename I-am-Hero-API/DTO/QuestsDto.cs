using I_am_Hero_API.Models;
namespace I_am_Hero_API.DTO
{
    public class QuestsDto : TokenDto
    {
        public IEnumerable<QuestDto> Quests { get; set; } = null!;

        public QuestsDto() { }
        public QuestsDto(IEnumerable<QuestDto> Quests)
        {
            this.Quests = Quests;
        }
        public QuestsDto(IEnumerable<Quest> Quests)
        {
            this.Quests = Quests.Select(x => new QuestDto(x)).ToList();
        }
    }
}
