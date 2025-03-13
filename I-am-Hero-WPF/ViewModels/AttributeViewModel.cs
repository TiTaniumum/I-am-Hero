using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using I_am_Hero_WPF.Models;

public class AttributeViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private HeroAttribute _selectedAttribute;
    public HeroAttribute SelectedAttribute
    {
        get => _selectedAttribute;
        set
        {
            _selectedAttribute = value;
            OnPropertyChanged(nameof(SelectedAttribute));
        }
    }
    private Visibility _editModalVisibility = Visibility.Collapsed;
    private Visibility _addModalVisibility = Visibility.Collapsed;
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

    public List<string> SortOptions { get; } = new List<string> { "None", "Name", "Value", "MinValue", "MaxValue" };

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


    private string _attributeName;
    private string _attributeDescription;
    private int _attributeMinValue;
    private int _attributeMaxValue;
    private int _attributeValue;
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

    public RelayCommand ClearSearchCommand { get; }
    public RelayCommand OpenAddModalCommand { get; }
    public RelayCommand CloseAddModalCommand { get; }
    public RelayCommand OpenEditModalCommand { get; }
    public RelayCommand CloseEditModalCommand { get; }
    public RelayCommand AddAttributeCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand ConfirmDeleteCommand { get; }

    private ObservableCollection<HeroAttribute> _attributes = new ObservableCollection<HeroAttribute>();
    public ObservableCollection<HeroAttribute> Attributes
    {
        get => _attributes;
        set
        {
            _attributes = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<HeroAttribute> _filteredAttributes = new ObservableCollection<HeroAttribute>();
    public ObservableCollection<HeroAttribute> FilteredAttributes
    {
        get => _filteredAttributes;
        set
        {
            _filteredAttributes = value;
            OnPropertyChanged();
        }
    }

    public AttributeViewModel()
    {
        _apiService = new ApiService();
        Attributes = new ObservableCollection<HeroAttribute>();
        FilteredAttributes = new ObservableCollection<HeroAttribute>();

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
        OpenAddModalCommand = new RelayCommand<long>(_ => { AddModalVisibility = Visibility.Visible; });
        SaveEditCommand = new RelayCommand(async _ => await EditAttribute());
        CloseEditModalCommand = new RelayCommand(_ => { EditModalVisibility = Visibility.Collapsed; });
        CloseAddModalCommand = new RelayCommand(_ => { AddModalVisibility = Visibility.Collapsed; });
        ConfirmDeleteCommand = new RelayCommand<long>(param =>
        {
            if (param is long id)
            {
                _ = DeleteAttribute(id);
            }
            else if (param is int intId)
            {
                _ = DeleteAttribute((long)intId);
            }
        });

        AddAttributeCommand = new RelayCommand(async _ => await AddAttribute());
        ClearSearchCommand = new RelayCommand(_ => ClearSearch());

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
                case "None":
                    break;
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

    private void OpenEditModal(long id)
    {
        SelectedAttribute = FilteredAttributes.FirstOrDefault(a => a.Id == id);
        EditModalVisibility = Visibility.Visible;
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
            CAttributeTypeId = 1
        };

        HttpResponseMessage response = await _apiService.CreateHeroAttributeAsync(newAttribute);

        if (response.IsSuccessStatusCode)
        {
            _ = LoadAttributes();
            AddModalVisibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show(response.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task EditAttribute()
    {
        if (SelectedAttribute == null) return;

        var response = await _apiService.EditHeroAttributeAsync(SelectedAttribute);
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Атрибут {SelectedAttribute.Id} обновлён!");
            _ = LoadAttributes();
            EditModalVisibility = Visibility.Collapsed;
        }
        else
        {
            Debug.WriteLine($"Ошибка обновления: {response.ReasonPhrase}");
        }
    }

    private async Task DeleteAttribute(long id)
    {
        var response = await _apiService.DeleteHeroAttributeAsync(id);
        if (response.IsSuccessStatusCode)
        {
            // Если не хотим делать запрос на сервер, а просто удалить атрибут из коллекции
            //var attributeToRemove = FilteredAttributes.FirstOrDefault(a => a.Id == id);

            //if (attributeToRemove != null)
            //{
            //    FilteredAttributes.Remove(attributeToRemove);
            //}

            _ = LoadAttributes();
        }
        else
        {
            Debug.WriteLine($"Ошибка удаления атрибута {id}: {response.ReasonPhrase}");
        }
    }

}

