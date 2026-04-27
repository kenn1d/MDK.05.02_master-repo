using Newtonsoft.Json;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        public void StartGame(object sender, RoutedEventArgs e)
        {   
            // если есть существующий UDP клиент — закроем его, чтобы предыдущий приёмник завершил работу
            if (MainWindow.mainWindow.receivingUdpClient != null)
            {
                MainWindow.mainWindow.receivingUdpClient.Close();
                MainWindow.mainWindow.receivingUdpClient = null;
            }

            // проверяем корректность введённых данных
            IPAddress UserIPAddress;
            if (!IPAddress.TryParse(ip.Text, out UserIPAddress))
            {
                MessageBox.Show("Используйте формат IP X.X.X.X");
                return;
            }
            int UserPort;
            if (!int.TryParse(port.Text, out UserPort))
            {
                MessageBox.Show("Порт должен быть числом");
                return;
            }

            // Сохраняем настройки пользователя перед запуском приёмника
            MainWindow.mainWindow.ViewModelUserSettings.IPAddress = ip.Text;
            MainWindow.mainWindow.ViewModelUserSettings.Port = port.Text;
            MainWindow.mainWindow.ViewModelUserSettings.Name = name.Text;

            // Запускаем приёмник (после того, как порт установлен)
            MainWindow.mainWindow.StartReceiver();

            // Отправляем команду старта на сервер
            MainWindow.Send("/start|" + JsonConvert.SerializeObject(MainWindow.mainWindow.ViewModelUserSettings));
        }
    }
}
