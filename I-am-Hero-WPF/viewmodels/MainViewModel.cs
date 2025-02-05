using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.models;
using I_am_Hero_WPF.Views;

public class MainViewModel : ViewModelBase
{
    public RelayCommand LogoutCommand { get; }
    private readonly ApiService _apiService;
    private string _heroName;
    private int _experience;
    private int _cLevelCalculationTypeId;

    public string HeroName
    {
        get => _heroName;
        set => SetProperty(ref _heroName, value);
    }

    public int Experience
    {
        get => _experience;
        set => SetProperty(ref _experience, value);
    }

    public int cLevelCalculationTypeId
    {
        get => _cLevelCalculationTypeId;
        set => SetProperty(ref _cLevelCalculationTypeId, value);
    }
    public MainViewModel()
    {
        _apiService = new ApiService();
        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken(); // Удаление токена перед выходом из аккаунта
            Application.Current.MainWindow.Content = new LoginPage(); // Возвращение на страницу входа
        });
        LoadHeroData();
    }

    private async Task LoadHeroData()
    {
        HttpResponseMessage response = await _apiService.GetHeroAsync();
        if (response.IsSuccessStatusCode)
        {
            string heroJson = await response.Content.ReadAsStringAsync();
            var hero = JsonSerializer.Deserialize<Hero>(heroJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (hero != null)
            {
                HeroName = hero.Name;
                Experience = hero.Experience;
                cLevelCalculationTypeId = hero.cLevelCalculationTypeId;
            }
        }
        else
        {
            MessageBox.Show("Не удалось загрузить данные героя: " + response.StatusCode, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
