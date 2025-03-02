using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
