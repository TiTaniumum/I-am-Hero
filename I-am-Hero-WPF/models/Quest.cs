using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace I_am_Hero_WPF.Models
{
    public class Quest : INotifyPropertyChanged
    {
        private bool _isExpanded;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Experience { get; set; }
        public QuestBehaviour CompletionQuestBehaviour { get; set; }
        public QuestBehaviour FailureQuestBehaviour { get; set; }
        public int Priority { get; set; }
        public long? CDifficultyId { get; set; }
        public long? CQuestStatusId { get; set; }
        public long? QuestLineId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ArchiveDate { get; set; }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ToggleExpandCommand { get; }

        public Quest()
        {
            ToggleExpandCommand = new RelayCommand(_ => IsExpanded = !IsExpanded);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
