using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class QuestLineDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public long? Experinece { get; set; }
        public QuestBehaviourDto? CompletionQuestBehaviour { get; set; }
        public QuestBehaviourDto? FailureQuestBehaviour { get; set; }
        public int? Priority { get; set; }
        public long? cDifficultyId { get; set; }
        public cDifficulty? cDifficulty { get; set; }
        public long? cQuestStatusId { get; set; }
        public cQuestStatus? cQuestStatus { get; set; }
        public QuestsDto? Quests { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
