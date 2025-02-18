namespace I_am_Hero_API.Models
{
    public class QuestLine
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public long Experience { get; set; }
        public long? CompletionBehaviourId { get; set; }
        public Behaviour? CompletionBehaviour { get; set; }
        public long? FailureBehaviourId { get; set; }
        public Behaviour? FailureBehaviour { get; set; }
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
