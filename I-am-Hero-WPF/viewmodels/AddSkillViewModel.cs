using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Models;
using I_am_Hero_WPF.Views;

public class AddSkillViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private string _skillName;
    private string _skillDescription;
    private int _experience;

    public string SkillName
    {
        get => _skillName;
        set => SetProperty(ref _skillName, value);
    }

    public string SkillDescription
    {
        get => _skillDescription;
        set => SetProperty(ref _skillDescription, value);
    }

    public int Experience
    {
        get => _experience;
        set => SetProperty(ref _experience, value);
    }

    public RelayCommand AddSkillCommand { get; }
    public RelayCommand CancelCommand { get; }

    public AddSkillViewModel()
    {
        _apiService = new ApiService();

        AddSkillCommand = new RelayCommand(async _ => await AddSkill());
        CancelCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.Content = new MainPage();
        });
    }

    private async Task AddSkill()
    {
        if (string.IsNullOrWhiteSpace(SkillName) || string.IsNullOrWhiteSpace(SkillDescription))
        {
            MessageBox.Show("Введите название и описание навыка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newSkill = new HeroSkill
        {
            Name = SkillName,
            Description = SkillDescription,
            Experience = Experience
        };

        HttpResponseMessage response = await _apiService.CreateHeroSkillAsync(newSkill);

        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show("Навык успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.MainWindow.Content = new MainPage();
        }
        else
        {
            MessageBox.Show("Ошибка при добавлении навыка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
