using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
    }
}
