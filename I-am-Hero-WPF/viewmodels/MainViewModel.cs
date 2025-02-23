using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Models;
using I_am_Hero_WPF.Views;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<double> VerticalLines { get; set; }
    public ObservableCollection<double> HorizontalLines { get; set; }



    public RelayCommand LogoutCommand { get; }
    public RelayCommand AddSkillCommand { get; }
    public RelayCommand AddQuestCommand { get; }
    private readonly ApiService _apiService;
    private string _heroName;
    private int _experience;
    private int _cLevelCalculationTypeId;
    private ObservableCollection<HeroSkill> _skills;
    private ObservableCollection<Quest> _quests;

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

    public ObservableCollection<HeroSkill> Skills
    {
        get => _skills;
        set => SetProperty(ref _skills, value);
    }
    public ObservableCollection<Quest> Quests
    {
        get => _quests;
        set => SetProperty(ref _quests, value);
    }

    public MainViewModel()
    {
        VerticalLines = new ObservableCollection<double>();
        HorizontalLines = new ObservableCollection<double>();

        // 8 вертикальных линий
        for (int i = 1; i <= 8; i++)
        {
            VerticalLines.Add(i * 100); // Через каждые 100px
        }

        // 10 горизонтальных линий (пример)
        for (int i = 1; i <= 10; i++)
        {
            HorizontalLines.Add(i * 80); // Через каждые 80px
        }



        _apiService = new ApiService();
        Skills = new ObservableCollection<HeroSkill>();

        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken();
            Application.Current.MainWindow.Content = new LoginPage();
        });
        AddSkillCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.Content = new AddSkillPage(); // Переход на AddSkillPage
        });
        AddQuestCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.Content = new AddQuestPage(); // Переход на AddQuestCommand
        });

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

        await LoadSkills(); 
        //await LoadQuests(); 
    }

    private async Task LoadSkills()
    {
        try
        {
            HttpResponseMessage response = await _apiService.GetHeroSkillsAsync();
            if (response.IsSuccessStatusCode)
            {
                string skillsJson = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<HeroSkillsResponse>(skillsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseObject?.HeroSkills != null)
                {
                    Skills = new ObservableCollection<HeroSkill>(responseObject.HeroSkills);
                }
                else
                {
                    Skills = new ObservableCollection<HeroSkill>(); // Пустая коллекция, если данных нет
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить навыки героя: " + response.StatusCode, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке навыков героя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadQuests()
    {
        try
        {
            HttpResponseMessage response = await _apiService.GetQuestsAsync();

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var quests = JsonSerializer.Deserialize<List<Quest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (quests != null)
                {
                    Quests.Clear();
                    foreach (var quest in quests)
                    {
                        Quests.Add(quest);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка загрузки квестов: " + response.StatusCode, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке квестов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}
