using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using I_am_Hero_WPF.Models;
using Microsoft.Win32;

public class ProfileViewModel : ViewModelBase
{
    public RelayCommand EditNicknameCommand { get; }
    public RelayCommand EditBiographyCommand { get; }
    public RelayCommand EditProfileDescriptionCommand { get; }
    public RelayCommand ChangeAvatarCommand { get; }
    private readonly ApiService _apiService;
    private string _heroName;
    private int _experience;
    private int _cLevelCalculationTypeId;

    private string _biography = "Твоя история...";
    private string _profileAvatar; // Составной аватар

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

    public string Biography
    {
        get => _biography;
        set { _biography = value; OnPropertyChanged(); }
    }

    public string ProfileImage
    {
        get => _profileAvatar;
        set { _profileAvatar = value; OnPropertyChanged(); }
    }

    public ProfileViewModel()
    {
        _apiService = new ApiService();
        EditNicknameCommand = new RelayCommand(_ =>
        {
            EditHeroName();
        });
        EditBiographyCommand = new RelayCommand(_ =>
        {
            EditBiography();
        });
        //ChangeAvatarCommand = new RelayCommand(ChangeAvatar);

        LoadHeroData();
    }

    private async Task LoadHeroData()
    {
        try
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
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке данных героя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    private void EditHeroName()
    {
        // Логика
        HeroName = "Новый ник"; // Заглушка
    }

    private void EditBiography()
    {
        // Логика
        Biography = "Обновленная биография...";
    }
}
