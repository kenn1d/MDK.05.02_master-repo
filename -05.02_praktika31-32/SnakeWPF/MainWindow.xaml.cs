using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Common;
using Newtonsoft.Json;

namespace SnakeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Главное окно, используется для общения между стртаницамм
        public static MainWindow mainWindow;

        /// <summary> Модель данных для передачи IP адреса устройства, порта, никнейма
        public ViewModelUserSettings ViewModelUserSettings = new ViewModelUserSettings();

        /// <summary> Модели игроков в которые передаются координаты змеи, яблок и точки ...
        //public ViewModelGames ViewModelGames = null;
        public List<ViewModelGames> ViewModelGamesList = null;
        public ViewModelGames PlayerSnake;

        /// <summary> Удалённый IP адрес для подключения к серверу
        public static IPAddress remoteIPAddress = IPAddress.Parse("127.0.0.1");

        /// <summary> Удалённый порт для подключения к серверу
        public static int remotePort = 5001;

        /// <summary> Основной поток для получения данных о игре
        public Thread tRec;

        /// <summary> UDP клиент для получения данных
        public UdpClient receivingUdpClient;

        /// <summary> Страница home
        public Pages.Home Home = new Pages.Home();
        /// <summary> Страница game
        public Pages.Game Game = new Pages.Game();

        // флаг запуска окна игры
        public bool gameStarted = false;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            OpenPage(Home);
        }

        public void StartReceiver()
        {
            tRec = new Thread(new ThreadStart(Receiver));
            tRec.IsBackground = true;
            tRec.Start();
        }

        public void OpenPage(Page PageOpen)
        {
            // Создаём анимацию
            DoubleAnimation startAnimation = new DoubleAnimation();
            // Задаём начальное значение анимации
            startAnimation.From = 1;
            // Задаём конечное значение анимации
            startAnimation.To = 0;
            // Задаём время анимации
            startAnimation.Duration = TimeSpan.FromSeconds(0.6);
            // Подписываемся на выполнение анимации
            startAnimation.Completed += delegate
            {
                frame.Navigate(PageOpen);
                DoubleAnimation endAnimation = new DoubleAnimation();
                endAnimation.From = 0;
                endAnimation.To = 1;
                endAnimation.Duration = TimeSpan.FromSeconds(0.6);
                frame.BeginAnimation(OpacityProperty, endAnimation);
            };
            frame.BeginAnimation(OpacityProperty, startAnimation);
        }

        public void Receiver()
        {
            // клиент для прослушивания
            receivingUdpClient = new UdpClient(int.Parse(ViewModelUserSettings.Port));
            // конечная сетевая точка
            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                // слушаем всегда
                while (true)
                {
                    // ожидание data
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    // преобразуем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);

                    // Конвертируем данные в модель
                    var gamesListPackage = JsonConvert.DeserializeObject<List<ViewModelGames>>(returnData);
                    if (gamesListPackage == null) continue;
                    ViewModelGamesList = gamesListPackage;
                    if (!gameStarted)
                    {
                        gameStarted = true;
                        Dispatcher.Invoke(() => OpenPage(Game));
                    }

                    var userSnake = ViewModelGamesList.FirstOrDefault(x => x.IdSnake == ViewModelUserSettings.IdSnake);
                    PlayerSnake = userSnake;
                    // если игра завершается
                    if (userSnake != null && userSnake.SnakesPlayers.GameOver)
                    {
                        gameStarted = false;
                        // выполняем вне потока
                        Dispatcher.Invoke(() =>
                        {
                            OpenPage(new Pages.EndGame());
                        });
                        break;
                    }
                    else if (gameStarted)
                    {
                        Game.CreateUI(ViewModelGamesList);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Возникло исключение: " + ex.ToString() + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// Отправляем команды
        /// </summary>
        /// <param name="datagram"></param>
        public static void Send(string datagram)
        {
            UdpClient sender = new UdpClient();
            // создаём endPoint по информации об удалённом хосте
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch(Exception ex)
            {
                // если клиент закрыт, значит программа была закрыта - не пишем в лог
                if (ex is SocketException || ex is ObjectDisposedException) return;
                Debug.WriteLine("Возникло исключение: " + ex.ToString() + "\n" + ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        /// <summary>
        /// Управление змеёй
        /// </summary>
        private void EventKeyUp(object sender, KeyEventArgs e)
        {
            // проверяем что игрок ввел все данные, и что игра не проиграна
            if(!string.IsNullOrEmpty(ViewModelUserSettings.IPAddress) &&
                !string.IsNullOrEmpty(ViewModelUserSettings.Port))
            {
                // если нажата клавиша вверх
                if (e.Key == Key.Up)
                    Send($"Up|{JsonConvert.SerializeObject(ViewModelUserSettings)}");
                // вниз
                if (e.Key == Key.Down)
                    Send($"Down|{JsonConvert.SerializeObject(ViewModelUserSettings)}");
                // влево
                if (e.Key == Key.Left)
                    Send($"Left|{JsonConvert.SerializeObject(ViewModelUserSettings)}");
                // вправо
                if (e.Key == Key.Right)
                    Send($"Right|{JsonConvert.SerializeObject(ViewModelUserSettings)}");
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void QuitApplication(object sender, System.ComponentModel.CancelEventArgs e)
        {
            receivingUdpClient.Close();
        }
    }
}