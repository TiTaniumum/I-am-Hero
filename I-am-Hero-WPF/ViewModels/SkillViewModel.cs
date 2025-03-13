using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using I_am_Hero_WPF.Models;
using System.Windows;

public class SkillViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private HeroSkill _selectedSkill;
    public HeroSkill SelectedSkill
    {
        get => _selectedSkill;
        set
        {
            _selectedSkill = value;
            OnPropertyChanged(nameof(SelectedSkill));
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

    public List<string> SortOptions { get; } = new List<string> { "None", "Name", "Experience" };

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


    private string _skillName;
    private string _skillDescription;
    private int _skillExperience;

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

    public RelayCommand ClearSearchCommand { get; }
    public RelayCommand OpenAddModalCommand { get; }
    public RelayCommand CloseAddModalCommand { get; }
    public RelayCommand OpenEditModalCommand { get; }
    public RelayCommand CloseEditModalCommand { get; }
    public RelayCommand OpenDeleteModalCommand { get; }
    public RelayCommand CloseDeleteModalCommand { get; }
    public RelayCommand AddSkillCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand ConfirmDeleteCommand { get; }

    private ObservableCollection<HeroSkill> _skills = new ObservableCollection<HeroSkill>();
    public ObservableCollection<HeroSkill> Skills
    {
        get => _skills;
        set
        {
            _skills = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<HeroSkill> _filteredSkills = new ObservableCollection<HeroSkill>();
    public ObservableCollection<HeroSkill> FilteredSkills
    {
        get => _filteredSkills;
        set
        {
            _filteredSkills = value;
            OnPropertyChanged();
        }
    }

    public SkillViewModel()
    {
        _apiService = new ApiService();
        Skills = new ObservableCollection<HeroSkill>();
        FilteredSkills = new ObservableCollection<HeroSkill>();
        SelectedSortOption = SortOptions.FirstOrDefault();

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
        SaveEditCommand = new RelayCommand(async _ => await EditSkill());
        CloseEditModalCommand = new RelayCommand(_ => { EditModalVisibility = Visibility.Collapsed; });
        CloseDeleteModalCommand = new RelayCommand(_ => { DeleteModalVisibility = Visibility.Collapsed; });
        CloseAddModalCommand = new RelayCommand(_ => { AddModalVisibility = Visibility.Collapsed; });
        ConfirmDeleteCommand = new RelayCommand<long>(async _ => await DeleteSkill());

        AddSkillCommand = new RelayCommand(async _ => await AddSkill());
        ClearSearchCommand = new RelayCommand(_ => ClearSearch());

        _ = LoadSkills();
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
                    ApplyFilters();
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

    private void ApplyFilters()
    {
        var filtered = Skills.ToList();

        if (!string.IsNullOrEmpty(SearchText))
        {
            filtered = filtered.Where(a => a.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        if (!string.IsNullOrEmpty(SelectedSortOption))
        {
            switch (SelectedSortOption)
            {
                case "None":
                    break;
                case "Name":
                    filtered = filtered.OrderBy(a => a.Name).ToList();
                    break;
                case "Experience":
                    filtered = filtered.OrderByDescending(a => a.Experience).ToList();
                    break;
            }
        }

        FilteredSkills = new ObservableCollection<HeroSkill>(filtered);
    }

    private void ClearSearch()
    {
        SearchText = string.Empty;
        SelectedSortOption = string.Empty;
        ApplyFilters();
    }

    private void OpenEditModal(long id)
    {
        SelectedSkill = FilteredSkills.FirstOrDefault(a => a.Id == id);
        EditModalVisibility = Visibility.Visible;
    }
    private void OpenDeleteModal(long id)
    {
        SelectedSkill = FilteredSkills.FirstOrDefault(a => a.Id == id);
        DeleteModalVisibility = Visibility.Visible;
    }

    private async Task AddSkill()
    {
        if (string.IsNullOrWhiteSpace(SkillName))
        {
            MessageBox.Show("Введите название умения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newSkill = new HeroSkill
        {
            Name = SkillName,
            Description = SkillDescription,
            Experience = SkillExperience,
            CLevelCalculationTypeId = 1
        };

        HttpResponseMessage response = await _apiService.CreateHeroSkillAsync(newSkill);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadSkills();
            AddModalVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task EditSkill()
    {
        if (SelectedSkill == null) return;

        var response = await _apiService.EditHeroSkillAsync(SelectedSkill);
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Умение {SelectedSkill.Id} обновлено!");
            _ = LoadSkills();
            EditModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка обновления: {response.ReasonPhrase}");
        }
    }

    private async Task DeleteSkill()
    {
        if (SelectedSkill == null) return;

        var response = await _apiService.DeleteHeroSkillAsync(SelectedSkill.Id);
        if (response.IsSuccessStatusCode)
        {
            _ = LoadSkills();
            DeleteModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка удаления умения {SelectedSkill.Id}: {response.ReasonPhrase}");
        }
    }
}
