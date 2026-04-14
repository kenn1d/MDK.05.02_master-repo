using MySql.Data.MySqlClient;
using praktika18.Classes;
using System.Windows;
using System.Windows.Controls;

namespace praktika18.Pages
{
    /// <summary>
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        public Create()
        {
            InitializeComponent();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = WorkingBD.Connection.OpenConnecuion();
            MySqlDataReader addQuery = WorkingBD.Connection.Query($"INSERT INTO `airlines`.`tickets` " +
                                                                  $"(`from`, `to`, price, timeStart, timeEnd) " +
                                                                  $"VALUES('{from.Text}', '{to.Text}', '{price.Text}', '{timeStart.Text}', '{timeEnd.Text}')", connection);
            WorkingBD.Connection.CloseConnection(connection);
            MainWindow.init.OpenPage(new Pages.Main());
        }
    }
}
