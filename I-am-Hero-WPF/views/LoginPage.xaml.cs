using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        private void GoToRegisterPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

    }
}
