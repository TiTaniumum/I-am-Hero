using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WPFLocalizeExtension.Engine;

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

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            string cultureCode = Thread.CurrentThread.CurrentUICulture.Name;
            switch (cultureCode)
            {
                case "en-US":
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
                    break;
                case "ru-RU":
                default:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    break;
            }
            
            Application.Current.MainWindow.Content = new LoginPage();
        }
    }
}