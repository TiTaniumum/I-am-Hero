using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using I_am_Hero_WPF.Models;

public class AttributeViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    public List<string> SortOptions { get; } = new List<string> { "Name", "Value", "MinValue", "MaxValue" };

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

    public RelayCommand ClearSearchCommand { get; }
    public RelayCommand ToggleExpandCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }

    private ObservableCollection<HeroAttribute> _attributes = new ObservableCollection<HeroAttribute>();
    public ObservableCollection<HeroAttribute> Attributes
    {
        get => _attributes;
        private set => SetProperty(ref _attributes, value);
    }

    private ObservableCollection<HeroAttribute> _filteredAttributes = new ObservableCollection<HeroAttribute>();
    public ObservableCollection<HeroAttribute> FilteredAttributes
    {
        get => _filteredAttributes;
        private set => SetProperty(ref _filteredAttributes, value);
    }

    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set { _isExpanded = value; OnPropertyChanged(); }
    }

    public AttributeViewModel()
    {
        _apiService = new ApiService();
        Attributes = new ObservableCollection<HeroAttribute>();
        FilteredAttributes = new ObservableCollection<HeroAttribute>();

        ClearSearchCommand = new RelayCommand(_ => ClearSearch());
        ToggleExpandCommand = new RelayCommand(_ => IsExpanded = !IsExpanded);
        EditCommand = new RelayCommand(_ => EditAttribute());
        DeleteCommand = new RelayCommand(_ => ConfirmDelete());

        _ = LoadAttributes();
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
                    ApplyFilters();
                }
            }
            else
            {
                MessageBox.Show($"Не удалось загрузить атрибуты героя: {response.StatusCode}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке атрибутов героя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ApplyFilters()
    {
        var filtered = Attributes.ToList();

        if (!string.IsNullOrEmpty(SearchText))
        {
            filtered = filtered.Where(a => a.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        if (!string.IsNullOrEmpty(SelectedSortOption))
        {
            switch (SelectedSortOption)
            {
                case "Name":
                    filtered = filtered.OrderBy(a => a.Name).ToList();
                    break;
                case "Value":
                    filtered = filtered.OrderByDescending(a => a.Value).ToList();
                    break;
                case "MinValue":
                    filtered = filtered.OrderBy(a => a.MinValue).ToList();
                    break;
                case "MaxValue":
                    filtered = filtered.OrderBy(a => a.MaxValue).ToList();
                    break;
            }
        }

        FilteredAttributes = new ObservableCollection<HeroAttribute>(filtered);
    }

    private void ClearSearch()
    {
        SearchText = string.Empty;
        SelectedSortOption = string.Empty;
        ApplyFilters();
    }

    private void EditAttribute()
    {
        // TODO: Модальное окно, редактирование 
    }

    private void ConfirmDelete()
    {
        // TODO: Модальное окно, удаление
    }
}
