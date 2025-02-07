using System.Windows;
using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
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
            Application.Current.MainWindow.Content = new RegisterPage
            {
                DataContext = new RegisterViewModel()
            };
        }

    }
}