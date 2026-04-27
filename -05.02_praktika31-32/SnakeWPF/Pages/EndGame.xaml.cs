using System.Windows;
using System.Windows.Controls;

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EndGame.xaml
    /// </summary>
    public partial class EndGame : Page
    {
        public EndGame()
        {
            InitializeComponent();
            // Выводим данные игрока
            name.Content = MainWindow.mainWindow.ViewModelUserSettings.Name;
            top.Content = MainWindow.mainWindow.PlayerSnake.Top;
            glasses.Content = $"{MainWindow.mainWindow.PlayerSnake.SnakesPlayers.Points.Count - 3} glasses";

            // Останавливаем поток
            MainWindow.mainWindow.receivingUdpClient.Close();
            // Обнуляем данные о змее
            MainWindow.mainWindow.PlayerSnake = null;
        }

        public void OpenHome(object sender, RoutedEventArgs e) =>
            MainWindow.mainWindow.OpenPage(MainWindow.mainWindow.Home);
    }
}
