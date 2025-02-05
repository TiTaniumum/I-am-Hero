using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Views;

public class LoginViewModel : ViewModelBase
{
    private string _email;
    private string _password;
    private readonly ApiService _apiService;

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
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
        var token = await _apiService.Login(Email, Password);
        if (!string.IsNullOrEmpty(token) && !token.StartsWith("Ошибка"))
        {
            TokenStorage.SaveToken(token);

            // Проверка на наличие героя
            HttpResponseMessage heroResponse = await _apiService.GetHeroAsync();
            if (heroResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                // Герой не найден – перенаправление на страницу создания героя
                Application.Current.MainWindow.Content = new CreateHeroPage();
            }
            else if (heroResponse.IsSuccessStatusCode)
            {
                // Герой найден – перенаправление на MainPage
                Application.Current.MainWindow.Content = new MainPage();
            }
            else
            {
                MessageBox.Show("Ошибка при получении информации о герое: " + heroResponse.StatusCode,
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show(token, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}