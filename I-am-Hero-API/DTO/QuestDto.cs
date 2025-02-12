using I_am_Hero_API.Models;
using Microsoft.CodeAnalysis;

namespace I_am_Hero_API.DTO
{
    public class QuestDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public long? Experinece { get; set; }
        public QuestBehaviourDto? CompletionQuestBehaviour { get; set; }
        public QuestBehaviourDto? FailureQuestBehaviour { get; set; }
        public int? Priority { get; set; }
        public long? cDifficultyId { get; set; }
        public long? cQuestStatusId { get; set; }
        public long? QuestLineId { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }

        public QuestDto(){}

        /// <summary>
        /// Only when quest is eager loaded
        /// </summary>
        /// <param name="quest"></param>
        public QuestDto(Quest quest)
        {
            Id = quest.Id;
            Title = quest.Title;
            Description = quest.Description;
            Experinece = quest.Experinece;
            if (quest.CompletionQuestBehaviour != null)
                CompletionQuestBehaviour = new QuestBehaviourDto(quest.CompletionQuestBehaviour);
            if (quest.FailureQuestBehaviour != null)
                FailureQuestBehaviour = new QuestBehaviourDto(quest.FailureQuestBehaviour);
            Priority = quest.Priority;
            cDifficultyId = quest.cDifficultyId;
            cQuestStatusId = quest.cQuestStatusId;
            QuestLineId = quest.QuestLineId;
            Deadline = quest.Deadline;
            CreateDate = quest.CreateDate;
            ArchiveDate = quest.ArchiveDate;
        }

        public QuestDto(Quest quest, QuestBehaviour? completion, QuestBehaviour? failure)
        {
            Id = quest.Id;
            Title = quest.Title;
            Description = quest.Description;
            Experinece = quest.Experinece;
            if(completion is not null)
                CompletionQuestBehaviour = new QuestBehaviourDto(completion);
            if(failure is not null)
                FailureQuestBehaviour = new QuestBehaviourDto(failure);
            Priority = quest.Priority;
            cDifficultyId = quest.cDifficultyId;
            cQuestStatusId = quest.cQuestStatusId;
            QuestLineId = quest.QuestLineId;
            Deadline = quest.Deadline;
            CreateDate = quest.CreateDate;
            ArchiveDate = quest.ArchiveDate;
        }
    }
}