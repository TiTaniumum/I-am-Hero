using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ControlzEx.Standard;

namespace I_am_Hero_WPF.Views
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
            {
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }


        private void GoToLoginPage(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new LoginPage
            {
                DataContext = new LoginViewModel()
            };
        }

    }
}