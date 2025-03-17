using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для QuestView.xaml
    /// </summary>
    public partial class QuestView : UserControl
    {
        public QuestView()
        {
            InitializeComponent();
            DataContext = new QuestViewModel();
        }
    }
}
