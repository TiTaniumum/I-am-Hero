using System.Windows;
using I_am_Hero_WPF.Views;

public class MainViewModel : ViewModelBase
{
    public string Email { get; set; }
    public RelayCommand LogoutCommand { get; }

    public MainViewModel(string email)
    {
        Email = email;

        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken(); // Удаление токена перед выходом из аккаунта
            Application.Current.MainWindow.Content = new LoginPage(); // Возвращение на страницу входа
        });
    }
}
