using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddQuestPage.xaml
    /// </summary>
    public partial class AddQuestPage : Page
    {
        public AddQuestPage()
        {
            InitializeComponent();
            DataContext = new CreateHeroViewModel();
        }
    }
}
