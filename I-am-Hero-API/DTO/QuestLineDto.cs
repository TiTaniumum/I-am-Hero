using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class QuestLineDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public long? Experinece { get; set; }
        public QuestBehaviourDto? CompletionQuestBehaviour { get; set; }
        public QuestBehaviourDto? FailureQuestBehaviour { get; set; }
        public int? Priority { get; set; }
        public long? cDifficultyId { get; set; }
        public long? cQuestStatusId { get; set; }
        public IEnumerable<QuestDto> Quests { get; set; } = null!;
        public DateTime? Deadline { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }

        public QuestLineDto() { }
        public QuestLineDto(QuestLine questLine)
        {
            Id = questLine.Id;
            Title = questLine.Title;
            Description = questLine.Description;
            Experinece = questLine.Experinece;
            if (questLine.CompletionQuestBehaviour != null)
                CompletionQuestBehaviour = new QuestBehaviourDto(questLine.CompletionQuestBehaviour);
            if (questLine.FailureQuestBehaviour != null)
                FailureQuestBehaviour = new QuestBehaviourDto(questLine.FailureQuestBehaviour);
            Priority = questLine.Priority;
            cDifficultyId = questLine.cDifficultyId;
            cQuestStatusId = questLine.cQuestStatusId;
            Quests = questLine.Quests.Select(x => new QuestDto(x)).ToList();
            Deadline = questLine.Deadline;
            CreateDate = questLine.CreateDate;
            ArchiveDate = questLine.ArchiveDate;
        }
    }
}
