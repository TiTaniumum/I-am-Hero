using System;
using System.Threading.Tasks;
using System.Windows;
using I_am_Hero_WPF.Models;
using I_am_Hero_WPF.Views;

public class AddQuestViewModel : ViewModelBase
{
    private readonly ApiService _apiService;

    private string _title;
    private string _description;
    private int _experience;
    private DateTime? _deadline;

    public RelayCommand SaveCommand { get; }
    public RelayCommand CancelCommand { get; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public int Experience
    {
        get => _experience;
        set => SetProperty(ref _experience, value);
    }

    public DateTime? Deadline
    {
        get => _deadline;
        set => SetProperty(ref _deadline, value);
    }

    public AddQuestViewModel()
    {
        _apiService = new ApiService();
        SaveCommand = new RelayCommand(async _ => await SaveQuest());
        CancelCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.Content = new MainPage(); 
        });
    }

    private async Task SaveQuest()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
        {
            MessageBox.Show("Введите название и описание квеста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newQuest = new Quest
        {
            Title = Title,
            Description = Description,
            Experience = Experience,
            Deadline = Deadline ?? DateTime.UtcNow.AddDays(7) // Дефолтный дедлайн через 7 дней
        };

        try
        {
            var success = await _apiService.CreateQuestAsync(newQuest);
            if (success.IsSuccessStatusCode)
            {
                MessageBox.Show("Квест успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                //NavigateBack();
            }
            else
            {
                MessageBox.Show("Не удалось добавить квест", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
