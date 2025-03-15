using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using I_am_Hero_WPF.Models;
using System.Windows;

public class QuestViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private Quest _selectedQuest;
    public Quest SelectedQuest
    {
        get => _selectedQuest;
        set
        {
            _selectedQuest = value;
            OnPropertyChanged(nameof(SelectedQuest));
        }
    }
    private Visibility _editModalVisibility = Visibility.Collapsed;
    private Visibility _addModalVisibility = Visibility.Collapsed;
    private Visibility _deleteModalVisibility = Visibility.Collapsed;
    public Visibility EditModalVisibility
    {
        get => _editModalVisibility;
        set
        {
            _editModalVisibility = value;
            OnPropertyChanged(nameof(EditModalVisibility));
        }
    }
    public Visibility AddModalVisibility
    {
        get => _addModalVisibility;
        set
        {
            _addModalVisibility = value;
            OnPropertyChanged(nameof(AddModalVisibility));
        }
    }
    public Visibility DeleteModalVisibility
    {
        get => _deleteModalVisibility;
        set
        {
            _deleteModalVisibility = value;
            OnPropertyChanged(nameof(DeleteModalVisibility));
        }
    }

    public List<string> SortOptions { get; } = new List<string> { "None", "Title", "Experience", "Priority", "Deadline" };

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }
    }

    private string _selectedSortOption;
    public string SelectedSortOption
    {
        get => _selectedSortOption;
        set
        {
            _selectedSortOption = value;
            OnPropertyChanged(nameof(SelectedSortOption));
            ApplyFilters();
        }
    }


    private string _questTitle;
    private string _questDescription;
    private int _questExperience;
    private QuestBehaviour _questCompletionBehaviour;
    private QuestBehaviour _questFailureBehaviour;
    private int _questPriority;
    private int _questCDifficultyId;
    private int _questCQuestStatusId;
    private int _questQuestLineId;
    private DateTime _questDeadline;
    private DateTime _questCreateDate;
    private DateTime? _questArchiveDate;

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

    public QuestBehaviour QuestCompletionBehaviour
    {
        get => _questCompletionBehaviour;
        set => SetProperty(ref _questCompletionBehaviour, value);
    }

    public QuestBehaviour QuestFailureBehaviour
    {
        get => _questFailureBehaviour;
        set => SetProperty(ref _questFailureBehaviour, value);
    }

    public int QuestPriority
    {
        get => _questPriority;
        set => SetProperty(ref _questPriority, value);
    }

    public int QuestCDifficultyId
    {
        get => _questCDifficultyId;
        set => SetProperty(ref _questCDifficultyId, value);
    }

    public int QuestCQuestStatusId
    {
        get => _questCQuestStatusId;
        set => SetProperty(ref _questCQuestStatusId, value);
    }

    public int QuestQuestLineId
    {
        get => _questQuestLineId;
        set => SetProperty(ref _questQuestLineId, value);
    }

    public DateTime QuestDeadline
    {
        get => _questDeadline;
        set => SetProperty(ref _questDeadline, value);
    }

    public DateTime QuestCreateDate
    {
        get => _questCreateDate;
        set => SetProperty(ref _questCreateDate, value);
    }

    public DateTime? QuestArchiveDate
    {
        get => _questArchiveDate;
        set => SetProperty(ref _questArchiveDate, value);
    }

    public RelayCommand ClearSearchCommand { get; }
    public RelayCommand OpenAddModalCommand { get; }
    public RelayCommand CloseAddModalCommand { get; }
    public RelayCommand OpenEditModalCommand { get; }
    public RelayCommand CloseEditModalCommand { get; }
    public RelayCommand OpenDeleteModalCommand { get; }
    public RelayCommand CloseDeleteModalCommand { get; }
    public RelayCommand AddQuestCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand ConfirmDeleteCommand { get; }
    public RelayCommand CompleteQuestCommand { get; }

    private ObservableCollection<Quest> _quests = new ObservableCollection<Quest>();
    public ObservableCollection<Quest> Quests
    {
        get => _quests;
        set
        {
            _quests = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Quest> _filteredQuests = new ObservableCollection<Quest>();
    public ObservableCollection<Quest> FilteredQuests
    {
        get => _filteredQuests;
        set
        {
            _filteredQuests = value;
            OnPropertyChanged();
        }
    }

    public QuestViewModel()
    {
        _apiService = new ApiService();
        Quests = new ObservableCollection<Quest>();
        FilteredQuests = new ObservableCollection<Quest>();
        SelectedSortOption = SortOptions.FirstOrDefault();
        QuestDeadline = DateTime.Now;

        OpenEditModalCommand = new RelayCommand<long>(param =>
        {
            if (param is long id)
            {
                OpenEditModal(id);
            }
            else if (param is int intId)
            {
                OpenEditModal((long)intId);
            }
        });
        OpenDeleteModalCommand = new RelayCommand<long>(param =>
        {
            if (param is long id)
            {
                OpenDeleteModal(id);
            }
            else if (param is int intId)
            {
                OpenDeleteModal((long)intId);
            }
        });
        OpenAddModalCommand = new RelayCommand<long>(_ => { AddModalVisibility = Visibility.Visible; });
        SaveEditCommand = new RelayCommand(async _ => await EditQuest());
        CloseEditModalCommand = new RelayCommand(_ => { EditModalVisibility = Visibility.Collapsed; });
        CloseDeleteModalCommand = new RelayCommand(_ => { DeleteModalVisibility = Visibility.Collapsed; });
        CloseAddModalCommand = new RelayCommand(_ => { AddModalVisibility = Visibility.Collapsed; });
        ConfirmDeleteCommand = new RelayCommand<long>(async _ => await DeleteQuest());
        CompleteQuestCommand = new RelayCommand<long>(param =>
        {
            if (param is long id)
            {
                _ = CompleteQuest(id);
            }
            else if (param is int intId)
            {
                _ = CompleteQuest((long)intId);
            }
        });

        AddQuestCommand = new RelayCommand(async _ => await AddQuest());
        ClearSearchCommand = new RelayCommand(_ => ClearSearch());

        _ = LoadQuests();
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
                    Quests = new ObservableCollection<Quest>(responseObject.Quests);
                    ApplyFilters();
                }
            }
            else
            {
                MessageBox.Show($"Не удалось загрузить квесты героя: {response.StatusCode}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке квестов героя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ApplyFilters()
    {
        var filtered = Quests.ToList();

        if (!string.IsNullOrEmpty(SearchText))
        {
            filtered = filtered.Where(a => a.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        if (!string.IsNullOrEmpty(SelectedSortOption))
        {
            switch (SelectedSortOption)
            {
                case "None":
                    break;
                case "Title":
                    filtered = filtered.OrderBy(a => a.Title).ToList();
                    break;
                case "Experience":
                    filtered = filtered.OrderByDescending(a => a.Experience).ToList();
                    break;
                case "Priority":
                    filtered = filtered.OrderBy(a => a.Priority).ToList();
                    break;
                case "Deadline":
                    filtered = filtered.OrderBy(a => a.Deadline).ToList();
                    break;
            }
        }

        FilteredQuests = new ObservableCollection<Quest>(filtered);
    }

    private void ClearSearch()
    {
        SearchText = string.Empty;
        SelectedSortOption = string.Empty;
        ApplyFilters();
    }

    private void OpenEditModal(long id)
    {
        SelectedQuest = FilteredQuests.FirstOrDefault(a => a.Id == id);
        EditModalVisibility = Visibility.Visible;
    }
    private void OpenDeleteModal(long id)
    {
        SelectedQuest = FilteredQuests.FirstOrDefault(a => a.Id == id);
        DeleteModalVisibility = Visibility.Visible;
    }

    private async Task AddQuest()
    {
        if (string.IsNullOrWhiteSpace(QuestTitle))
        {
            MessageBox.Show("Введите название квеста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newQuest = new Quest
        {
            Title = QuestTitle,
            Description = QuestDescription,
            Experience = QuestExperience,
            Priority = QuestPriority,
            Deadline = QuestDeadline
        };

        HttpResponseMessage response = await _apiService.CreateHeroQuestAsync(newQuest);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadQuests();
            AddModalVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task EditQuest()
    {
        if (SelectedQuest == null) return;

        var response = await _apiService.EditHeroQuestAsync(SelectedQuest);
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Квест {SelectedQuest.Id} обновлён!");
            _ = LoadQuests();
            EditModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка обновления: {response.ReasonPhrase}");
        }
    }

    private async Task DeleteQuest()
    {
        if (SelectedQuest == null) return;

        var response = await _apiService.DeleteHeroQuestAsync(SelectedQuest.Id);
        if (response.IsSuccessStatusCode)
        {
            _ = LoadQuests();
            DeleteModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка удаления квеста {SelectedQuest.Id}: {response.ReasonPhrase}");
        }
    }

    private async Task CompleteQuest(long id)
    {
        SelectedQuest = FilteredQuests.FirstOrDefault(a => a.Id == id);

        SelectedQuest.CQuestStatusId = 3;
        var questResponse = await _apiService.EditHeroQuestAsync(SelectedQuest);

        if (!questResponse.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Ошибка обновления квеста {SelectedQuest.Id}: {questResponse.ReasonPhrase}");
            return;
        }

        Debug.WriteLine($"Квест {SelectedQuest.Id} завершён!");

        var currentExperience = await _apiService.GetHeroExperienceAsync();
        if (currentExperience == null)
        {
            Debug.WriteLine("Ошибка: не удалось получить текущий опыт героя.");
            return;
        }

        long newExperience = currentExperience.Value + SelectedQuest.Experience;
        var experienceResponse = await _apiService.UpdateHeroExperienceAsync(newExperience);

        if (!experienceResponse.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Ошибка обновления опыта: {experienceResponse.ReasonPhrase}");
            return;
        }

        Debug.WriteLine($"Опыт героя обновлён: {currentExperience} → {newExperience}");

        _ = LoadQuests();
    }
}
