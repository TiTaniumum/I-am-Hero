using System.Windows;

public class MainViewModel : ViewModelBase
{
    public string Username { get; set; }

    public RelayCommand LogoutCommand { get; }

    public MainViewModel(string username)
    {
        Username = username;

        LogoutCommand = new RelayCommand(_ =>
        {
            // Доработать логику выхода
            Application.Current.Shutdown(); 
        });
    }
}
