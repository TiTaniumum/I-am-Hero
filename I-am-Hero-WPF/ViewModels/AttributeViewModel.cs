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
using System.Windows.Controls;
using System.Windows.Input;
using ControlzEx.Standard;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
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
    public Visibility EditModalVisibility
    {
        get => _editModalVisibility;
        set
        {
            _editModalVisibility = value;
            OnPropertyChanged(nameof(EditModalVisibility));
        }
    }

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
    public RelayCommand OpenEditDialogCommand { get; }
    public RelayCommand CloseEditDialogCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand EditAttributeCommand { get; }
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

        OpenEditDialogCommand = new RelayCommand<long>(param =>
        {
            if (param is long id)
            {
                OpenEditDialog(id);
            }
            else if (param is int intId)
            {
                OpenEditDialog((long)intId);
            }
        });
        SaveEditCommand = new RelayCommand(async _ => await EditAttribute());
        CloseEditDialogCommand = new RelayCommand(_ => CloseEditDialog());
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

    private void OpenEditDialog(long id)
    {
        SelectedAttribute = FilteredAttributes.FirstOrDefault(a => a.Id == id);
        EditModalVisibility = Visibility.Visible;
    }
    private void CloseEditDialog()
    {
        EditModalVisibility = Visibility.Collapsed;
    }

    private async Task EditAttribute()
    {
        if (SelectedAttribute == null) return;

        var response = await _apiService.EditHeroAttributeAsync(SelectedAttribute);
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Атрибут {SelectedAttribute.Id} обновлён!");
            EditModalVisibility = Visibility.Collapsed;
            _ = LoadAttributes();
        }
        else
        {
            Debug.WriteLine($"Ошибка обновления: {response.StatusCode}");
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

