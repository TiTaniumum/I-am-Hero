using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace I_am_Hero_WPF.Models
{
    public class HeroSkill : INotifyPropertyChanged
    {
        private bool _isExpanded;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        public int CLevelCalculationTypeId { get; set; }

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

        public HeroSkill()
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
