using praktika27.ViewModell;
using System.Windows.Controls;

namespace praktika27.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            this.DataContext = new VMItems();
        }
    }
}
