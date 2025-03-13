using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для EffectView.xaml
    /// </summary>
    public partial class EffectView : UserControl
    {
        public EffectView()
        {
            InitializeComponent();
            DataContext = new EffectViewModel();
        }
    }
}
