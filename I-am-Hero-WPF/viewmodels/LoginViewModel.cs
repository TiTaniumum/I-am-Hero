using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Views;

public class LoginViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private readonly ApiService _apiService;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public RelayCommand LoginCommand { get; }

    public LoginViewModel()
    {
        _apiService = new ApiService();

        LoginCommand = new RelayCommand(async _ => await Login());
    }

    private async Task Login()
    {
        var result = await _apiService.Login(Username, Password);

        if (!string.IsNullOrEmpty(result) && !result.Contains("Ошибка"))
        {
            MessageBox.Show($"Добро пожаловать, {result}!");

            // Переход
            Application.Current.MainWindow.Content = new MainPage
            {
                DataContext = new MainViewModel(result)
            };
        }
        else
        {
            MessageBox.Show(result, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
