using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_am_Hero_WPF.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        public QuestBehaviour CompletionQuestBehaviour { get; set; }
        public QuestBehaviour FailureQuestBehaviour { get; set; }
        public int Priority { get; set; }
        public int CDifficultyId { get; set; }
        public int CQuestStatusId { get; set; }
        public int QuestLineId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
