using System.Windows;
using System.Windows.Controls;
using I_am_Hero_WPF.Views;

public class MainViewModel : ViewModelBase
{
    public RelayCommand LogoutCommand { get; }
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

    private double _sidebarUnfoldedWidth = 175;
    private double _sidebarFoldedWidth = 10 + 24 + 10; // Начальная ширина, свернутое состояние (10 - Margin, 24 - Width of icon, 10 - Margin)
    private double _sidebarWidth;
    private bool _sidebarExpanded = false;
    private string _sidebarArrowIcon = "ChevronRight";

    private UserControl _currentContent;
    
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

    public UserControl CurrentContent
    {
        get => _currentContent;
        set => SetProperty(ref _currentContent, value);
    }


    public MainViewModel()
    {
        _sidebarWidth = _sidebarFoldedWidth;
        CurrentContent = new DashboardView();

        LogoutCommand = new RelayCommand(_ =>
        {
            TokenStorage.DeleteToken();
            Application.Current.MainWindow.Content = new LoginPage();
        });
        ToggleSidebarCommand = new RelayCommand(_ =>
        {
            ToggleSidebar();
        });
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
    }

    private void ToggleSidebar()
    {
        SidebarExpanded = !SidebarExpanded;
        SidebarWidth = SidebarExpanded ? _sidebarUnfoldedWidth : _sidebarFoldedWidth;
        SidebarArrowIcon = SidebarExpanded ? "ChevronLeft" : "ChevronRight";
    }
}
