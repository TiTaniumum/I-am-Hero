using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using I_am_Hero_WPF.Models;
using I_am_Hero_WPF.Views;
using System.Threading;
using System.Globalization;

public class MainViewModel : ViewModelBase
{
    private readonly ApiService _apiService;
    public RelayCommand LogoutCommand { get; }
    public RelayCommand OpenSettingsModalCommand { get; }
    public RelayCommand CloseSettingsModalCommand { get; }
    public RelayCommand OpenLogoutModalCommand { get; }
    public RelayCommand CloseLogoutModalCommand { get; }
    public RelayCommand ToggleSidebarCommand { get; }
    public RelayCommand NavigateToProfileCommand { get; }
    public RelayCommand NavigateToDashboardCommand { get; }
    public RelayCommand NavigateToSkillCommand { get; }
    public RelayCommand NavigateToAttributeCommand { get; }
    public RelayCommand NavigateToEffectCommand { get; }
    public RelayCommand NavigateToDailyCommand { get; }
    public RelayCommand NavigateToQuestCommand { get; }
    public RelayCommand NavigateToQuestLineCommand { get; }
    public RelayCommand NavigateToHabbitCommand { get; }
    public RelayCommand NavigateToAchievementCommand { get; }
    public RelayCommand OpenAboutCommand { get; }
    public RelayCommand OpenSupportCommand { get; }

    private double _sidebarUnfoldedWidth = 175;
    private double _sidebarFoldedWidth = 10 + 24 + 10; // Начальная ширина, свернутое состояние (10 - Margin, 24 - Width of icon, 10 - Margin)
    private double _sidebarWidth;
    private bool _sidebarExpanded = false;
    private string _sidebarArrowIcon = "ChevronRight";

    private Visibility _settingsModalVisibility = Visibility.Collapsed;
    private Visibility _logoutModalVisibility = Visibility.Collapsed;
    private UserControl _currentContent;

    public List<string> AvailableLanguages { get; } = new List<string> { "English", "Русский" };
    private List<string> _experienceCalculationMethods = new List<string>();
    private string _selectedExperienceMethod = string.Empty;
    private string _selectedLanguage;
    private bool _notificationsEnabled = true;
    public string NotificationsText => NotificationsEnabled ? "Включено" : "Выключено";

    public double SidebarWidth
    {
        get => _sidebarWidth;
        set
        {
            _sidebarWidth = value;
            OnPropertyChanged();
        }
    }

    public bool SidebarExpanded
    {
        get => _sidebarExpanded;
        set
        {
            _sidebarExpanded = value;
            OnPropertyChanged();
        }
    }

    public string SidebarArrowIcon
    {
        get => _sidebarArrowIcon;
        set
        {
            _sidebarArrowIcon = value;
            OnPropertyChanged();
        }
    }
    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (SetProperty(ref _selectedLanguage, value))
            {
                ChangeLanguage(value);
            }
        }
    }

    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        set
        {
            SetProperty(ref _notificationsEnabled, value);
            OnPropertyChanged(nameof(NotificationsText));
        }
    }
    public UserControl CurrentContent
    {
        get => _currentContent;
        set => SetProperty(ref _currentContent, value);
    }
    public Visibility SettingsModalVisibility
    {
        get => _settingsModalVisibility;
        set
        {
            _settingsModalVisibility = value;
            OnPropertyChanged(nameof(SettingsModalVisibility));
        }
    }
    public Visibility LogoutModalVisibility
    {
        get => _logoutModalVisibility;
        set
        {
            _logoutModalVisibility = value;
            OnPropertyChanged(nameof(LogoutModalVisibility));
        }
    }
    public List<string> ExperienceCalculationMethods
    {
        get => _experienceCalculationMethods;
        set => SetProperty(ref _experienceCalculationMethods, value);
    }
    public string SelectedExperienceMethod
    {
        get => _selectedExperienceMethod;
        set => SetProperty(ref _selectedExperienceMethod, value);
    }


    public MainViewModel()
    {
        _apiService = new ApiService();
        _sidebarWidth = _sidebarFoldedWidth;
        CurrentContent = new DashboardView();

        switch (Thread.CurrentThread.CurrentUICulture.Name)
        {
            case "ru-RU":
                _selectedLanguage = "Русский";
                break;
            case "en-US":
                _selectedLanguage = "English";
                break;
            default:
                _selectedLanguage = "Русский";
                break;
        }

        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken();
            Application.Current.MainWindow.Content = new LoginPage();
        });
        OpenSettingsModalCommand = new RelayCommand<long>(_ => { SettingsModalVisibility = Visibility.Visible; });
        OpenLogoutModalCommand = new RelayCommand<long>(_ => { LogoutModalVisibility = Visibility.Visible; });
        CloseSettingsModalCommand = new RelayCommand<long>(_ => { SettingsModalVisibility = Visibility.Collapsed; });
        CloseLogoutModalCommand = new RelayCommand<long>(_ => { LogoutModalVisibility = Visibility.Collapsed; });
        ToggleSidebarCommand = new RelayCommand(_ => ToggleSidebar());
        NavigateToProfileCommand = new RelayCommand(_ =>
        {
            CurrentContent = new ProfileView();
        });
        NavigateToDashboardCommand = new RelayCommand(_ =>
        {
            CurrentContent = new DashboardView();
        });
        NavigateToSkillCommand = new RelayCommand(_ =>
        {
            CurrentContent = new SkillView();
        });
        NavigateToEffectCommand = new RelayCommand(_ =>
        {
            CurrentContent = new EffectView();
        });
        NavigateToAttributeCommand = new RelayCommand(_ =>
        {
            CurrentContent = new AttributeView();
        });
        NavigateToDailyCommand = new RelayCommand(_ =>
        {
            CurrentContent = new DailyView();
        });
        NavigateToQuestCommand = new RelayCommand(_ =>
        {
            CurrentContent = new QuestView();
        });
        NavigateToQuestLineCommand = new RelayCommand(_ =>
        {
            CurrentContent = new QuestLineView();
        });
        NavigateToHabbitCommand = new RelayCommand(_ =>
        {
            CurrentContent = new HabbitView();
        });
        NavigateToAchievementCommand = new RelayCommand(_ =>
        {
            CurrentContent = new AchievementView();
        });
        OpenAboutCommand = new RelayCommand(_ => OpenAboutPage());
        OpenSupportCommand = new RelayCommand(_ => OpenSupportPage());

        _ = LoadExperienceCalculationMethods();
    }

    private void ChangeLanguage(string language)
    {
        string cultureCode;
        switch (language)
        {
            case "Русский":
                cultureCode = "ru-RU";
                break;
            case "English":
                cultureCode = "en-US";
                break;
            default:
                cultureCode = "ru-RU";
                break;
        }

        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        OnPropertyChanged(nameof(CurrentCultureDisplay));

        foreach (var dict in Application.Current.Resources.MergedDictionaries)
        {
            dict.Source = dict.Source;
        }

        if (Application.Current.MainWindow != null)
        {
            Application.Current.MainWindow.Content = new MainPage();
        }
    }

    public string CurrentCultureDisplay
    {
        get { return Thread.CurrentThread.CurrentUICulture.DisplayName; }
    }

    private void ToggleSidebar()
    {
        SidebarExpanded = !SidebarExpanded;
        SidebarWidth = SidebarExpanded ? _sidebarUnfoldedWidth : _sidebarFoldedWidth;
        SidebarArrowIcon = SidebarExpanded ? "ChevronLeft" : "ChevronRight";
    }

    private void OpenAboutPage()
    {
        string url = "https://github.com/TiTaniumum/I-am-Hero";
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    private void OpenSupportPage()
    {
        string url = "https://boosty.to";
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

    public async Task LoadExperienceCalculationMethods()
    {
        try
        {
            HttpResponseMessage response = await _apiService.GetLevelCalculationTypesAsync();

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var methods = JsonSerializer.Deserialize<List<ExperienceCalculationMethod>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (methods != null && methods.Count > 0)
                {
                    ExperienceCalculationMethods = methods.Select(m => m.NameRu).ToList();
                    SelectedExperienceMethod = ExperienceCalculationMethods.First();
                }
            }
            else
            {
                Debug.WriteLine($"Ошибка загрузки данных: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка при получении данных: {ex.Message}");
        }
    }

}
