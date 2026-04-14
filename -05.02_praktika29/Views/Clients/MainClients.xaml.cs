using System.Windows.Controls;

namespace praktika29.Views.Clients
{
    /// <summary>
    /// Логика взаимодействия для MainClients.xaml
    /// </summary>
    public partial class MainClients : Page
    {
        public MainClients(object Context)
        {
            InitializeComponent();
            DataContext = Context;
        }
    }
}
