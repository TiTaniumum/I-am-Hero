using System.Windows;
using I_am_Hero_WPF.Views;

public class MainViewModel : ViewModelBase
{
    public RelayCommand LogoutCommand { get; }

    public MainViewModel()
    {

        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken(); // Удаление токена перед выходом из аккаунта
            Application.Current.MainWindow.Content = new LoginPage(); // Возвращение на страницу входа
        });
    }
}
