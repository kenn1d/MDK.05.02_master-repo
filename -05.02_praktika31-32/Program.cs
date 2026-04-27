using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common;
using Newtonsoft.Json;

namespace praktika31_32
{
    public class Program
    {
        /// <summary>
        /// Коллекция рекордов
        /// </summary>
        public static List<Leaders> Leaders = new List<Leaders>();
        /// <summary>
        /// Коллекция, содержащая IP адрес игрока, порт ...
        /// </summary>
        public static List<ViewModelUserSettings> remoteIpAddress = new List<ViewModelUserSettings>();
        /// <summary>
        /// Коллекция, содержащая точки змеи, точку на карте
        /// </summary>
        public static List<ViewModelGames> viewModelGames = new List<ViewModelGames>();
        /// <summary>
        /// Локальный порт, который прослушивается для ответов
        /// </summary>
        private static int localPort = 5001;
        /// <summary>
        /// Максимальная скорость движения змейки
        /// </summary>
        private static int MaxSpeed = 15;

        /// <summary> Функция, рассылающая данные пользователям
        private static void Send(List<ViewModelGames> allGames)
        {
            if (allGames == null || allGames.Count == 0) return;
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(allGames));
            foreach (ViewModelUserSettings User in remoteIpAddress)
            {
                // Создаём UdpClient
                UdpClient sender = new UdpClient();
                // Создаём endPoint по информации об удалённом хосте
                IPEndPoint endPoint = new IPEndPoint(
                    IPAddress.Parse(User.IPAddress),
                    int.Parse(User.Port));
                try
                {
                    // Отправляем данные
                    sender.Send(bytes, bytes.Length, endPoint);
                    // Выводим ответ в консоль
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Отправил данные пользователю: {User.IPAddress}:{User.Port}");
                }
                catch (Exception ex) {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n " + ex.Message);
                }
                finally { 
                    sender.Close();
                }
            }
        }

        /// <summary> Метод приёма сообщений
        public static void Recevier()
        {
            // Создаём UdpClient
            UdpClient receivingUdpClient = new UdpClient(localPort);
            // Создаём endPoint по информации об удалённом хосте
            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                // Выводим сообщение
                Console.WriteLine("Команды сервера:");
                // Запускаем беконечный цикл для прослушки входящих сообщений
                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] reciveBytes = receivingUdpClient.Receive(
                        ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(reciveBytes);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Получил команду: " + returnData.ToString());

                    // Начало игры
                    if (returnData.ToString().Contains("/start"))
                    {
                        // Делим данные на команду и данные Json
                        string[] dataMessage = returnData.Split('|');
                        // Конвертируем данные в модель
                        ViewModelUserSettings viewModelUserSettings = JsonConvert.DeserializeObject<ViewModelUserSettings>(dataMessage[1]);
                        // Выводим запись в консоль
                        Console.WriteLine($"Подключился пользователь: {viewModelUserSettings.IPAddress}:{viewModelUserSettings.Port}");
                        // Добавляем данные в коллекцию чтобы отправить пользователю
                        remoteIpAddress.Add(viewModelUserSettings);
                        // Добавляем змею
                        viewModelUserSettings.IdSnake = AddSnake();
                        // Связываем змею и игрока
                        viewModelGames[viewModelUserSettings.IdSnake].IdSnake = viewModelUserSettings.IdSnake;
                    }
                    else // Если команда не стартовая
                    {
                        // Управление змеёй
                        string[] dataMessage = returnData.Split('|');

                        // Конвертируем данные в модель
                        ViewModelUserSettings viewModelUserSettings = JsonConvert.DeserializeObject<ViewModelUserSettings>(dataMessage[1]);
                        int IdPlayer = -1;

                        // В случае если мёртвый игрок присылает команду
                        // Находим id игрока, ища его в списке по ip адресу и порту
                        IdPlayer = remoteIpAddress.FindIndex(x => x.IPAddress == viewModelUserSettings.IPAddress 
                            && x.Port == viewModelUserSettings.Port);
                        // Если игрок найден
                        if (IdPlayer != -1)
                        {
                            // Если команда вверх, и если змея не ползёт вниз
                            if (dataMessage[0] == "Up" &&
                                viewModelGames[IdPlayer].SnakesPlayers.direction != Snakes.Direction.Down)
                                // Змее игрока указываем команду вверх
                                viewModelGames[IdPlayer].SnakesPlayers.direction = Snakes.Direction.Up;
                            else if (dataMessage[0] == "Down" &&
                                viewModelGames[IdPlayer].SnakesPlayers.direction != Snakes.Direction.Up)
                                viewModelGames[IdPlayer].SnakesPlayers.direction = Snakes.Direction.Down;
                            else if (dataMessage[0] == "Left" &&
                                viewModelGames[IdPlayer].SnakesPlayers.direction != Snakes.Direction.Right)
                                viewModelGames[IdPlayer].SnakesPlayers.direction = Snakes.Direction.Left;
                            else if (dataMessage[0] == "Right" &&
                                viewModelGames[IdPlayer].SnakesPlayers.direction != Snakes.Direction.Left)
                                viewModelGames[IdPlayer].SnakesPlayers.direction = Snakes.Direction.Right;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n " + ex.Message);
            }
        }

        ///<summary> Функция добавления змеи
        public static int AddSnake()
        {
            ViewModelGames viewModelGamesPlayer = new ViewModelGames();
            viewModelGamesPlayer.SnakesPlayers = new Snakes()
            {
                Points = new List<Snakes.Point>() {
                    new Snakes.Point() { X = 30, Y = 10 },
                    new Snakes.Point() { X = 20, Y = 10 },
                    new Snakes.Point() { X = 10, Y = 10 }
                },
                direction = Snakes.Direction.Start
            };
            // Создаём рандомную точку на карте
            viewModelGamesPlayer.Points = new Snakes.Point(new Random().Next(10, 783), new Random().Next(10, 410));
            // Добавляем змею в общий список всех змей
            viewModelGames.Add(viewModelGamesPlayer);
            // Возвращаем id змеи, чтобы связать игрока и змею
            return viewModelGames.FindIndex(x => x ==  viewModelGamesPlayer);
        }

        public static void Timer()
        {
            while (true)
            {
                // останаваливаем на 100 мс
                Thread.Sleep(100);

                // Получаем змей, которых нужно удалить
                List<ViewModelGames> RemoveSnakes = viewModelGames.FindAll(x => x.SnakesPlayers.GameOver);
                if(RemoveSnakes.Count > 0)
                {
                    foreach (ViewModelGames DeadSnake in RemoveSnakes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Отключил пользователя: {remoteIpAddress.Find(x => x.IdSnake == DeadSnake.IdSnake).IPAddress}" + 
                            $":{remoteIpAddress.Find(x => x.IdSnake == DeadSnake.IdSnake).Port}");
                        // Удаляем пользователя
                        viewModelGames.RemoveAll(x => x.SnakesPlayers.GameOver);
                    }
                    // Удаляем змей
                    viewModelGames.RemoveAll(x => x.SnakesPlayers.GameOver);
                }

                // Перебираем подключенных игроков
                foreach (ViewModelUserSettings User in remoteIpAddress.ToList())
                {
                    var gameEntry = viewModelGames.FirstOrDefault(x => x.IdSnake == User.IdSnake);

                    // Если сессии нет или змея уже мертва, то скипаем итерацию
                    if (gameEntry == null || gameEntry.SnakesPlayers.GameOver)
                    {
                        continue;
                    }
                    Snakes Snake = gameEntry.SnakesPlayers;


                    for (int i = Snake.Points.Count - 1; i >= 0; i--)
                    {
                        // если у нас не первая точка
                        if(i != 0)
                        {
                            Snake.Points[i] = Snake.Points[i - 1];
                        }
                        else
                        {
                            // получаем сколкьрость змеи
                            int Speed = 10 + (int)Math.Round(Snake.Points.Count/20f);
                            // если скорость более максимальной
                            if(Speed > MaxSpeed) Speed = MaxSpeed;

                            // если направление змеи влево
                            if (Snake.direction == Snakes.Direction.Right)
                            {
                                // двиагем влево
                                Snake.Points[i] = new Snakes.Point() { X = Snake.Points[i].X + Speed, Y = Snake.Points[i].Y  };
                            }

                            // если направление змеи вниз
                            if (Snake.direction == Snakes.Direction.Down)
                            {
                                // двиагем вниз
                                Snake.Points[i] = new Snakes.Point() { X = Snake.Points[i].X, Y = Snake.Points[i].Y + Speed };
                            }

                            // если направление змеи вправо
                            if (Snake.direction == Snakes.Direction.Up)
                            {
                                // двиагем вправо
                                Snake.Points[i] = new Snakes.Point() { X = Snake.Points[i].X, Y = Snake.Points[i].Y - Speed };
                            }

                            // если направление змеи влево
                            if (Snake.direction == Snakes.Direction.Left)
                            {
                                // двиагем влево
                                Snake.Points[i] = new Snakes.Point() { X = Snake.Points[i].X - Speed, Y = Snake.Points[i].Y };
                            }
                        }
                    }

                    // проверяем змею на столкновние
                    // если первая точка змеи вышла за координаты экрана по горизонатали
                    if (Snake.Points[0].X <= 0 || Snake.Points[0].Y >= 793)
                    {
                        Snake.GameOver = true;
                    }
                    else if (Snake.Points[0].Y <= 0 || Snake.Points[0].Y >= 420)
                    {
                        Snake.GameOver = true;
                    }

                    // проверяем что мы столкнулись сами с собой
                    if (Snake.direction != Snakes.Direction.Start)
                    {
                        // прогоняем все точки кроме первой
                        for (int i = 1; i < Snake.Points.Count; i++) { 
                            // если первая точка находится в координатах последующей по горизонтали
                            if (Snake.Points[0].X >= Snake.Points[i].X - 1 && Snake.Points[0].X <= Snake.Points[i].X + 1)
                            {
                                // по вертикали
                                if (Snake.Points[0].Y >= Snake.Points[i].Y - 1 && Snake.Points[0].Y <= Snake.Points[i].Y + 1)
                                {
                                    Snake.GameOver = true;
                                    break;
                                }
                            }
                        }
                    }

                    // Проверяем что если первая точка змеи игрока находится в координатах яблока по горизонтали
                    if (Snake.Points[0].X >= viewModelGames.Find(x => x.IdSnake == User.IdSnake).Points.X - 15 &&
                        Snake.Points[0].X <= viewModelGames.Find(x => x.IdSnake == User.IdSnake).Points.X + 15)
                    {
                        // Проверяем что если первая точка змеи игрока находится в координатах яблока по вертикали
                        if (Snake.Points[0].Y >= viewModelGames.Find(x => x.IdSnake == User.IdSnake).Points.Y - 15 &&
                            Snake.Points[0].Y <= viewModelGames.Find(x => x.IdSnake == User.IdSnake).Points.Y + 15)
                        {
                            // создаём новое яблоко
                            viewModelGames.Find(x => x.IdSnake == User.IdSnake).Points = new Snakes.Point(
                                new Random().Next(10, 783),
                                new Random().Next(10, 410));
                            // добавляем змее новую точку на координатах последней
                            Snake.Points.Add(new Snakes.Point()
                            {
                                X = Snake.Points[Snake.Points.Count - 1].X,
                                Y = Snake.Points[Snake.Points.Count - 1].Y
                            });

                            // загружаем таблицу
                            LoadLeaders();
                            // добавляем нас в таблицу
                            Leaders.Add(new Leaders()
                            {
                                Name = User.Name,
                                Points = Snake.Points.Count - 3
                            });
                            Leaders = Leaders.OrderByDescending(x => x.Points).ThenBy(x => x.Name).ToList();
                            viewModelGames.Find(x => x.IdSnake == User.IdSnake).Top =
                                Leaders.FindIndex(x => x.Points == Snake.Points.Count - 3 && x.Name == User.Name) + 1;
                        }
                    }

                    // елси игра закончена
                    if (Snake.GameOver)
                    {
                        LoadLeaders();
                        Leaders.Add(new Leaders()
                        {
                            Name = User.Name,
                            Points = Snake.Points.Count - 3
                        });
                        SaveLeaders();
                    }
                }
                Send(viewModelGames);
            }
        }

        public static void SaveLeaders()
        {
            string json = JsonConvert.SerializeObject(Leaders);
            StreamWriter SW = new StreamWriter("./leaders.txt");
            SW.WriteLine(json);
            SW.Close();
        }

        public static void LoadLeaders()
        {
            if (File.Exists("./leaders.txt"))
            {
                StreamReader SR = new StreamReader("./leaders.txt");
                string json = SR.ReadLine();
                SR.Close();
                if (!string.IsNullOrEmpty(json))
                {
                    Leaders = JsonConvert.DeserializeObject<List<Leaders>>(json);
                }
                else
                {
                    Leaders = new List<Leaders>();
                }
            }
            else
                Leaders = new List<Leaders>();
        }

        static void Main(string[] args)
        {
            try
            {
                // Создаём поток для просулшивания сообщений от клиентов
                Thread tRec = new Thread(new ThreadStart(Recevier));
                // Запускаем поток прослушивания
                tRec.Start();

                // Создаём таймер для управления игрой
                Thread tTime = new Thread(Timer);
                // Запускаем таймер для управления игрой
                tTime.Start();
            }
            catch (Exception ex) { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n" + ex.Message);
            }
        }
    }
}
