using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Views;

public class RegisterViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _confirmPassword;
    private readonly ApiService _apiService;

    public string Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            SetProperty(ref _confirmPassword, value);
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand RegisterCommand { get; }

    public RegisterViewModel()
    {
        _apiService = new ApiService();

        RegisterCommand = new RelayCommand(async _ => await Register(), _ => CanRegister());
    }

    private bool CanRegister()
    {
        return !string.IsNullOrEmpty(Username) &&
               !string.IsNullOrEmpty(Password) &&
               !string.IsNullOrEmpty(ConfirmPassword) &&
               Password == ConfirmPassword;
    }

    private async Task Register()
    {
        var result = await _apiService.Register(Username, Password);

        if (result.Contains("успешна"))
        {
            MessageBox.Show(result, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Переход на страницу авторизации
            Application.Current.MainWindow.Content = new LoginPage();
        }
        else
        {
            MessageBox.Show(result, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
