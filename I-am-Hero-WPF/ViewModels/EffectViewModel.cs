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
using System.Windows.Media.Effects;

internal class EffectViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private HeroStatusEffect _selectedEffect;
    public HeroStatusEffect SelectedEffect
    {
        get => _selectedEffect;
        set
        {
            _selectedEffect = value;
            OnPropertyChanged(nameof(SelectedEffect));
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

    public List<string> SortOptions { get; } = new List<string> { "None", "Name", "Value" };

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


    private string _effectName;
    private string _effectDescription;
    private int _effectValue;

    public string EffectName
    {
        get => _effectName;
        set => SetProperty(ref _effectName, value);
    }
    public string EffectDescription
    {
        get => _effectDescription;
        set => SetProperty(ref _effectDescription, value);
    }
    public int EffectValue
    {
        get => _effectValue;
        set => SetProperty(ref _effectValue, value);
    }

    public RelayCommand ClearSearchCommand { get; }
    public RelayCommand OpenAddModalCommand { get; }
    public RelayCommand CloseAddModalCommand { get; }
    public RelayCommand OpenEditModalCommand { get; }
    public RelayCommand CloseEditModalCommand { get; }
    public RelayCommand OpenDeleteModalCommand { get; }
    public RelayCommand CloseDeleteModalCommand { get; }
    public RelayCommand AddEffectCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand ConfirmDeleteCommand { get; }

    private ObservableCollection<HeroStatusEffect> _effects = new ObservableCollection<HeroStatusEffect>();
    public ObservableCollection<HeroStatusEffect> Effects
    {
        get => _effects;
        set
        {
            _effects = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<HeroStatusEffect> _filteredEffects = new ObservableCollection<HeroStatusEffect>();
    public ObservableCollection<HeroStatusEffect> FilteredEffects
    {
        get => _filteredEffects;
        set
        {
            _filteredEffects = value;
            OnPropertyChanged();
        }
    }

    public EffectViewModel()
    {
        _apiService = new ApiService();
        Effects = new ObservableCollection<HeroStatusEffect>();
        FilteredEffects = new ObservableCollection<HeroStatusEffect>();
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
        SaveEditCommand = new RelayCommand(async _ => await EditEffect());
        CloseEditModalCommand = new RelayCommand(_ => { EditModalVisibility = Visibility.Collapsed; });
        CloseDeleteModalCommand = new RelayCommand(_ => { DeleteModalVisibility = Visibility.Collapsed; });
        CloseAddModalCommand = new RelayCommand(_ => { AddModalVisibility = Visibility.Collapsed; });
        ConfirmDeleteCommand = new RelayCommand<long>(async _ => await DeleteEffect());

        AddEffectCommand = new RelayCommand(async _ => await AddEffect());
        ClearSearchCommand = new RelayCommand(_ => ClearSearch());

        _ = LoadEffects();
    }

    private async Task LoadEffects()
    {
        try
        {
            HttpResponseMessage response = await _apiService.GetHeroEffectsAsync();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<HeroStatusEffectsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (responseObject?.HeroStatusEffects != null)
                {
                    Effects = new ObservableCollection<HeroStatusEffect>(responseObject.HeroStatusEffects);
                    ApplyFilters();
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить статус эффекты героя: " + response.StatusCode, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке статус эффектов героя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ApplyFilters()
    {
        var filtered = Effects.ToList();

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
                case "Value":
                    filtered = filtered.OrderByDescending(a => a.Value).ToList();
                    break;
            }
        }

        FilteredEffects = new ObservableCollection<HeroStatusEffect>(filtered);
    }

    private void ClearSearch()
    {
        SearchText = string.Empty;
        SelectedSortOption = string.Empty;
        ApplyFilters();
    }

    private void OpenEditModal(long id)
    {
        SelectedEffect = FilteredEffects.FirstOrDefault(a => a.Id == id);
        EditModalVisibility = Visibility.Visible;
    }
    private void OpenDeleteModal(long id)
    {
        SelectedEffect = FilteredEffects.FirstOrDefault(a => a.Id == id);
        DeleteModalVisibility = Visibility.Visible;
    }

    private async Task AddEffect()
    {
        if (string.IsNullOrWhiteSpace(EffectName))
        {
            MessageBox.Show("Введите название статус эффекта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newEffect = new HeroStatusEffect
        {
            Name = EffectName,
            Description = EffectDescription,
            Value = EffectValue
        };

        HttpResponseMessage response = await _apiService.CreateHeroEffectAsync(newEffect);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadEffects();
            AddModalVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task EditEffect()
    {
        if (SelectedEffect == null) return;

        var response = await _apiService.EditHeroEffectAsync(SelectedEffect);
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Статус эффект {SelectedEffect.Id} обновлено!");
            _ = LoadEffects();
            EditModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка обновления: {response.ReasonPhrase}");
        }
    }

    private async Task DeleteEffect()
    {
        if (SelectedEffect == null) return;

        var response = await _apiService.DeleteHeroEffectAsync(SelectedEffect.Id);
        if (response.IsSuccessStatusCode)
        {
            _ = LoadEffects();
            DeleteModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка удаления статус эффекта {SelectedEffect.Id}: {response.ReasonPhrase}");
        }
    }
}


