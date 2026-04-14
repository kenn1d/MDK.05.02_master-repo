using System.Windows.Controls;

namespace praktika29.Views.Orders
{
    /// <summary>
    /// Логика взаимодействия для MainOrders.xaml
    /// </summary>
    public partial class MainOrders : Page
    {
        public MainOrders(object Context)
        {
            InitializeComponent();
            DataContext = Context;
        }
    }
}
