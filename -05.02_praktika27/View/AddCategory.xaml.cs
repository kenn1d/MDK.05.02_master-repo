using System.Windows.Controls;

namespace praktika27.View
{
    /// <summary>
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Page
    {
        public AddCategory(object context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
