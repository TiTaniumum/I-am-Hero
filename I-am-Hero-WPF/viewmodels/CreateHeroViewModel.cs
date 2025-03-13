using System;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Views;

public class CreateHeroViewModel : ViewModelBase
{
    private string _heroName;
    private readonly ApiService _apiService;

    public string HeroName
    {
        get => _heroName;
        set
        {
            SetProperty(ref _heroName, value);
            CreateHeroCommand.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand CreateHeroCommand { get; }

    public CreateHeroViewModel()
    {
        _apiService = new ApiService();
        CreateHeroCommand = new RelayCommand(async _ => await CreateHero(), _ => CanCreateHero());
    }

    private bool CanCreateHero()
    {
        return !string.IsNullOrWhiteSpace(HeroName);
    }

    private async Task CreateHero()
    {
        string result = await _apiService.CreateHeroAsync(HeroName);

        if (!string.IsNullOrEmpty(result) && !result.StartsWith("Ошибка"))
        {
            Application.Current.MainWindow.Content = new MainPage();
        }
        else
        {
            MessageBox.Show(result, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
