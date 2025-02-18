using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddSkillPage.xaml
    /// </summary>
    public partial class AddSkillPage : Page
    {
        public AddSkillPage()
        {
            InitializeComponent();
            DataContext = new CreateHeroViewModel();
        }
    }
}
