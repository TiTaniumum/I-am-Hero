using System.Threading.Tasks;
using I_am_Hero_WPF.Views;
using System.Windows.Controls;

public static class AuthManager
{
    private static readonly ApiService _apiService = new ApiService();

    public static async Task<Page> GetStartupPage()
    {
        string token = TokenStorage.LoadToken();

        if (!string.IsNullOrEmpty(token) && await _apiService.IsTokenValid())
        {
            return new MainPage(); // Авторизован
        }
        else
        {
            return new LoginPage(); // Нужно войти
        }
    }
}
