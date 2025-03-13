using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace I_am_Hero_WPF.Models
{
    public class HeroAttribute : INotifyPropertyChanged
    {
        private bool _isExpanded;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CAttributeTypeId { get; set; }
        public int MinValue { get; set; }
        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int? CurrentStateId { get; set; }

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

        public HeroAttribute()
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
