using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для SkillView.xaml
    /// </summary>
    public partial class SkillView : UserControl
    {
        public SkillView()
        {
            InitializeComponent();
            DataContext = new SkillViewModel();
        }
    }
}
