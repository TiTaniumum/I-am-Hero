using System.Windows.Controls;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AttributeView.xaml
    /// </summary>
    public partial class AttributeView : UserControl
    {
        public AttributeView()
        {
            InitializeComponent();
            DataContext = new AttributeViewModel();
        }
    }
}
