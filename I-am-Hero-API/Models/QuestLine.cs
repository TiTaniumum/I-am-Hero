namespace I_am_Hero_API.Models
{
    public class QuestLine
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public long Experinece { get; set; }
        public long? CompletionQuestBehaviourId { get; set; }
        public QuestBehaviour? CompletionQuestBehaviour { get; set; }
        public long? FailureQuestBehaviourId { get; set; }
        public QuestBehaviour? FailureQuestBehaviour { get; set; }
        public int? Priority { get; set; }
        public long? cDifficultyId { get; set; }
        public cDifficulty? cDifficulty { get; set; }
        public long? cQuestStatusId { get; set; }
        public cQuestStatus? cQuestStatus { get; set; }
        public IEnumerable<Quest> Quests { get; set; } = new List<Quest>();
        public DateTime? Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
