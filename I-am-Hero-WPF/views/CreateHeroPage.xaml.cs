using System.Windows;
using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    public partial class CreateHeroPage : Page
    {
        public CreateHeroPage()
        {
            InitializeComponent();
            DataContext = new CreateHeroViewModel();
        }
    }
}
