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
    public RelayCommand AddAttributeCommand { get; }
    public RelayCommand AddQuestCommand { get; }
    public RelayCommand OpenSkillModalCommand { get; }
    public RelayCommand CloseSkillModalCommand { get; }
    public RelayCommand OpenAttributeModalCommand { get; }
    public RelayCommand CloseAttributeModalCommand { get; }
    public RelayCommand OpenQuestModalCommand { get; }
    public RelayCommand CloseQuestModalCommand { get; }
    public RelayCommand ToggleEditModeCommand { get; }

    private readonly ApiService _apiService;

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set { _isEditMode = value; OnPropertyChanged(nameof(IsEditMode)); }
    }

    private string _heroName;
    private int _heroExperience;
    private int _cLevelCalculationTypeId;
    private ObservableCollection<HeroSkill> _skills;
    private ObservableCollection<HeroAttribute> _attributes;
    private ObservableCollection<Quest> _quests;
    public string HeroName
    {
        get => _heroName;
        set => SetProperty(ref _heroName, value);
    }
    public int HeroExperience
    {
        get => _heroExperience;
        set => SetProperty(ref _heroExperience, value);
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
    public ObservableCollection<HeroAttribute> Attributes
    {
        get => _attributes;
        set => SetProperty(ref _attributes, value);
    }
    public ObservableCollection<Quest> Quests
    {
        get => _quests;
        set => SetProperty(ref _quests, value);
    }

    private Visibility _addSkillVisibility = Visibility.Collapsed;
    private string _skillName;
    private string _skillDescription;
    private int _skillExperience;
    public Visibility AddSkillVisibility
    {
        get => _addSkillVisibility;
        set
        {
            _addSkillVisibility = value;
            OnPropertyChanged(nameof(AddSkillVisibility));
        }
    }
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
    public int SkillExperience
    {
        get => _skillExperience;
        set => SetProperty(ref _skillExperience, value);
    }


    private Visibility _addAttributeVisibility = Visibility.Collapsed;
    private string _attributeName;
    private string _attributeDescription;
    private int _attributeMinValue;
    private int _attributeMaxValue;
    private int _attributeValue;
    public Visibility AddAttributeVisibility
    {
        get => _addAttributeVisibility;
        set
        {
            _addAttributeVisibility = value;
            OnPropertyChanged(nameof(AddAttributeVisibility));
        }
    }
    public string AttributeName
    {
        get => _attributeName;
        set => SetProperty(ref _attributeName, value);
    }
    public string AttributeDescription
    {
        get => _attributeDescription;
        set => SetProperty(ref _attributeDescription, value);
    }
    public int AttributeMinValue
    {
        get => _attributeMinValue;
        set => SetProperty(ref _attributeMinValue, value);
    }
    public int AttributeMaxValue
    {
        get => _attributeMaxValue;
        set => SetProperty(ref _attributeMaxValue, value);
    }
    public int AttributeValue
    {
        get => _attributeValue;
        set => SetProperty(ref _attributeValue, value);
    }

    private Visibility _addQuestVisibility = Visibility.Collapsed;
    private string _questTitle;
    private string _questDescription;
    private int _questExperience;

    public Visibility AddQuestVisibility
    {
        get => _addQuestVisibility;
        set
        {
            _addQuestVisibility = value;
            OnPropertyChanged(nameof(AddQuestVisibility));
        }
    }
    public string QuestTitle
    {
        get => _questTitle;
        set => SetProperty(ref _questTitle, value);
    }
    public string QuestDescription
    {
        get => _questDescription;
        set => SetProperty(ref _questDescription, value);
    }
    public int QuestExperience
    {
        get => _questExperience;
        set => SetProperty(ref _questExperience, value);
    }


    public DashboardViewModel()
    {
        _apiService = new ApiService();
        Skills = new ObservableCollection<HeroSkill>();
        Attributes = new ObservableCollection<HeroAttribute>();
        Quests = new ObservableCollection<Quest>();

        OpenSkillModalCommand = new RelayCommand(_ => { AddSkillVisibility = Visibility.Visible; });
        CloseSkillModalCommand = new RelayCommand(_ => { AddSkillVisibility = Visibility.Collapsed; });

        OpenAttributeModalCommand = new RelayCommand(_ => { AddAttributeVisibility = Visibility.Visible; });
        CloseAttributeModalCommand = new RelayCommand(_ => { AddAttributeVisibility = Visibility.Collapsed; });

        OpenQuestModalCommand = new RelayCommand(_ => { AddQuestVisibility = Visibility.Visible; });
        CloseQuestModalCommand = new RelayCommand(_ => { AddQuestVisibility = Visibility.Collapsed; });

        ToggleEditModeCommand = new RelayCommand(_ => { IsEditMode = !IsEditMode; });

        AddSkillCommand = new RelayCommand(async _ => await AddSkill());
        AddAttributeCommand = new RelayCommand(async _ => await AddAttribute());
        //AddQuestCommand = new RelayCommand(async _ => await AddQuest());

        _ = LoadData();
    }

    private async Task LoadData()
    {
        await LoadHero();
        await LoadSkills();
        await LoadAttributes();
        await LoadQuests();
    }
    private async Task LoadHero()
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
                    HeroExperience = hero.Experience;
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
    private async Task LoadAttributes()
    {
        try
        {
            HttpResponseMessage response = await _apiService.GetHeroAttributesAsync();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<HeroAttributesResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseObject?.HeroAttributes != null)
                {
                    Attributes = new ObservableCollection<HeroAttribute>(responseObject.HeroAttributes);
                }
                else
                {
                    Attributes = new ObservableCollection<HeroAttribute>(); // Пустая коллекция, если данных нет
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить аттрибуты героя: " + response.StatusCode, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке аттрибутов героя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var responseObject = JsonSerializer.Deserialize<QuestsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseObject?.Quests != null)
                {
                    Quests = new ObservableCollection<Quest>(responseObject.Quests);
                }
                else
                {
                    Quests = new ObservableCollection<Quest>(); // Пустая коллекция, если данных нет
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

    private async Task AddSkill()
    {
        if (string.IsNullOrWhiteSpace(SkillName))
        {
            MessageBox.Show("Введите название навыка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newSkill = new HeroSkill
        {
            Name = SkillName,
            Description = SkillDescription,
            Experience = SkillExperience,
            CLevelCalculationTypeId = cLevelCalculationTypeId
        };

        HttpResponseMessage response = await _apiService.CreateSkillAsync(newSkill);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadData();
            AddSkillVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private async Task AddAttribute()
    {
        if (string.IsNullOrWhiteSpace(AttributeName))
        {
            MessageBox.Show("Введите название аттрибута.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newAttribute = new HeroAttribute
        {
            Name = AttributeName,
            Description = AttributeDescription,
            MinValue = AttributeMinValue,
            MaxValue = AttributeMaxValue,
            Value = AttributeValue,
            CAttributeTypeId = cLevelCalculationTypeId
        };

        HttpResponseMessage response = await _apiService.CreateAttributeAsync(newAttribute);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadData();
            AddAttributeVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
