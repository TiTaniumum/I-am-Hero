using System.Threading;
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

        private void OnEnglishClick(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Application.Current.MainWindow.Content = new LoginPage();
        }
        private void OnRussianClick(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            Application.Current.MainWindow.Content = new LoginPage();
        }
    }
}