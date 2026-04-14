using System.Windows.Controls;

namespace praktika27.View
{
    /// <summary>
    /// Логика взаимодействия для MainCategory.xaml
    /// </summary>
    public partial class MainCategory : Page
    {
        public MainCategory()
        {
            InitializeComponent();
            DataContext = new ViewModell.VMCategorys();
        }
    }
}
