using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF;
using I_am_Hero_WPF.Models;
using I_am_Hero_WPF.Views;

public class DashboardViewModel : ViewModelBase
{
    private readonly DashboardView _dashboardView;

    // Commands
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
    public RelayCommand ToggleMenuCommand { get; }
    public RelayCommand ResetCommand { get; }
    public RelayCommand ApplyChangesCommand { get; }

    private readonly ApiService _apiService;

    // Hero
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

    // Skills
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

    // Attributes
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

    // Quests
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

    // Sidebar
    private bool _sidebarExpanded;
    private double _sidebarWidth;
    private string _sidebarArrowIcon;

    private const double _sidebarUnfoldedWidth = 200;
    private const double _sidebarFoldedWidth = 30;
    private bool _isEditMode;
    private int _rows;
    private int _columns;
    private int _minRows = 6;
    private int _minColumns = 9;

    public bool SidebarExpanded
    {
        get => _sidebarExpanded;
        set { _sidebarExpanded = value; OnPropertyChanged(); }
    }
    public double SidebarWidth
    {
        get => _sidebarWidth;
        set { _sidebarWidth = value; OnPropertyChanged(); }
    }
    public string SidebarArrowIcon
    {
        get => _sidebarArrowIcon;
        set { _sidebarArrowIcon = value; OnPropertyChanged(); }
    }
    public bool IsEditMode
    {
        get => _isEditMode;
        set { _isEditMode = value; OnPropertyChanged(nameof(IsEditMode)); }
    }
    public int Rows
    {
        get => _rows;
        set
        {
            if (_rows != value)
            {
                if (value < MinRows) value = MinRows;
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }
    }
    public int Columns
    {
        get => _columns;
        set
        {
            if (_columns != value)
            {
                if (value < MinColumns) value = MinColumns;
                _columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }
    }
    public int MinRows
    {
        get => _minRows;
        set
        {
            if (_minRows != value)
            {
                _minRows = value;
                OnPropertyChanged(nameof(MinRows));
            }
        }
    }
    public int MinColumns
    {
        get => _minColumns;
        set
        {
            if (_minColumns != value)
            {
                _minColumns = value;
                OnPropertyChanged(nameof(MinColumns));
            }
        }
    }

    //Blocks
    private double _profileLeft;
    public double ProfileLeft
    {
        get => _profileLeft;
        set { _profileLeft = value; OnPropertyChanged(); }
    }

    private double _profileTop;
    public double ProfileTop
    {
        get => _profileTop;
        set { _profileTop = value; OnPropertyChanged(); }
    }


    public DashboardViewModel(DashboardView dashboardView)
    {
        _dashboardView = dashboardView;
        Rows = _dashboardView.GetRows();
        Columns = _dashboardView.GetColumns();

        _apiService = new ApiService();

        Skills = new ObservableCollection<HeroSkill>();
        Attributes = new ObservableCollection<HeroAttribute>();
        Quests = new ObservableCollection<Quest>();

        SidebarExpanded = false;
        SidebarWidth = _sidebarFoldedWidth;
        SidebarArrowIcon = "ChevronLeft";

        OpenSkillModalCommand = new RelayCommand(_ => { AddSkillVisibility = Visibility.Visible; });
        CloseSkillModalCommand = new RelayCommand(_ => { AddSkillVisibility = Visibility.Collapsed; });

        OpenAttributeModalCommand = new RelayCommand(_ => { AddAttributeVisibility = Visibility.Visible; });
        CloseAttributeModalCommand = new RelayCommand(_ => { AddAttributeVisibility = Visibility.Collapsed; });

        OpenQuestModalCommand = new RelayCommand(_ => { AddQuestVisibility = Visibility.Visible; });
        CloseQuestModalCommand = new RelayCommand(_ => { AddQuestVisibility = Visibility.Collapsed; });

        AddSkillCommand = new RelayCommand(async _ => await AddSkill());
        AddAttributeCommand = new RelayCommand(async _ => await AddAttribute());
        //AddQuestCommand = new RelayCommand(async _ => await AddQuest());

        //ToggleEditModeCommand = new RelayCommand(_ => { IsEditMode = !IsEditMode; });
        ToggleMenuCommand = new RelayCommand(_ => ToggleSidebar());
        ResetCommand = new RelayCommand(_ => ResetDashboard());
        ApplyChangesCommand = new RelayCommand(_ => ApplyChanges());

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
            HttpResponseMessage response = await _apiService.GetHeroQuestsAsync();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<QuestsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseObject?.Quests != null)
                {
                    var filteredQuests = responseObject.Quests
                    .Where(quest => quest.CQuestStatusId != 3)
                    .ToList();

                    Quests = new ObservableCollection<Quest>(filteredQuests);
                    //Quests = new ObservableCollection<Quest>(responseObject.Quests);
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

        HttpResponseMessage response = await _apiService.CreateHeroSkillAsync(newSkill);

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

        HttpResponseMessage response = await _apiService.CreateHeroAttributeAsync(newAttribute);

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

    private void ToggleSidebar()
    {
        IsEditMode = !IsEditMode;
        SidebarExpanded = !SidebarExpanded;
        SidebarWidth = SidebarExpanded ? _sidebarUnfoldedWidth : _sidebarFoldedWidth;
        SidebarArrowIcon = SidebarExpanded ? "ChevronRight" : "ChevronLeft";
    }
    private void ResetDashboard()
    {
        _dashboardView.SetRows(6);
        _dashboardView.SetColumns(9);
        Rows = _dashboardView.GetRows();
        Columns = _dashboardView.GetColumns();
    }

    private void ApplyChanges()
    {
        _dashboardView.SetRows(Rows);
        _dashboardView.SetColumns(Columns);
    }
}
