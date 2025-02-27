using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Models;

public class DashboardViewModel : ViewModelBase
{
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
    public DashboardViewModel()
    {
        _apiService = new ApiService();
        Skills = new ObservableCollection<HeroSkill>();
        AddSkillCommand = new RelayCommand(_ =>
        {
            // Добавить модальное окно
            //Application.Current.MainWindow.Content = new AddSkillPage(); 
        });
        AddQuestCommand = new RelayCommand(_ =>
        {
            // Добавить модальное окно
            //Application.Current.MainWindow.Content = new AddQuestPage(); 
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
                string json = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<HeroSkillsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
                var responseObject = JsonSerializer.Deserialize<List<Quest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseObject != null)
                {
                    Quests.Clear();
                    foreach (var quest in responseObject)
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
