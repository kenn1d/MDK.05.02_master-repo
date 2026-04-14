using System.Windows.Controls;

namespace praktika29.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для MainProducts.xaml
    /// </summary>
    public partial class MainProducts : Page
    {
        public MainProducts(object Context)
        {
            InitializeComponent();
            DataContext = Context;
        }
    }
}
