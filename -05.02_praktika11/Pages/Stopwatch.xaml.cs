using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace praktika11.Pages
{
    /// <summary>
    /// Логика взаимодействия для Stopwatch.xaml
    /// </summary>
    public partial class Stopwatch : Page
    {
        public DispatcherTimer dispatcherTimer = new DispatcherTimer(); // объявляем таймер
        public DispatcherTimer dispatcherTimerInterval = new DispatcherTimer();
        public float full_second = 0; // прошедшее время
        public float full_secondInterval = 0;
        public bool start_stopwatch = false; // переменная старта
        public Classes.Times MyTime = new Classes.Times(); // экземпляр класса с данными о текущем времени

        public Stopwatch()
        {
            InitializeComponent();
            
            dispatcherTimer.Tick += TimerSecond; // присваиваем функцию для выполнения
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);  // устанавливаем время для выполнения
            dispatcherTimerInterval.Tick += TimerInterval; // присваиваем функцию для выполнения
            dispatcherTimerInterval.Interval = new TimeSpan(0, 0, 1);  // устанавливаем время для выполнения
        }

        /// <summary>
        /// Функция таймера для выполнения
        /// </summary>
        private void TimerSecond(object sender, EventArgs e)
        {
            full_second++; // увеличиваем время

            // выводим время на экран
            float hours = (int)(full_second / 60 / 60); // рассчитываем часы
            float minuts = (int)(full_second / 60) - (hours * 60); // рассчитываем минуты
            float seconds = full_second - (hours * 60 * 60) - (minuts * 60); // рассчитываем секунды

            string s_seconds = seconds.ToString(); // присваиваем как текст
            if (seconds < 10) s_seconds = "0" + seconds; // добавляем ноль если меньше 10

            string s_minuts = minuts.ToString();
            if (minuts < 10) s_minuts = "0" + minuts;

            string s_hours = hours.ToString();
            if (hours < 10) s_hours = "0" + hours;

            time.Content = s_hours + ':' + s_minuts + ':' + s_seconds; // выводим на экран
        }
        private void TimerInterval(object sender, EventArgs e)
        {
            full_secondInterval++; // увеличиваем время

            // выводим время на экран
            float hours = (int)(full_secondInterval / 60 / 60); // рассчитываем часы
            float minuts = (int)(full_secondInterval / 60) - (hours * 60); // рассчитываем минуты
            float seconds = full_secondInterval - (hours * 60 * 60) - (minuts * 60); // рассчитываем секунды

            string s_seconds = seconds.ToString(); // присваиваем как текст
            if (seconds < 10) s_seconds = "0" + seconds; // добавляем ноль если меньше 10

            string s_minuts = minuts.ToString();
            if (minuts < 10) s_minuts = "0" + minuts;

            string s_hours = hours.ToString();
            if (hours < 10) s_hours = "0" + hours;

            MyTime.Time = s_hours + ':' + s_minuts + ':' + s_seconds; // Добавляем текущее время в экземпляр
        }

        private void StartStopwatch(object sender, RoutedEventArgs e)
        {
            if (start_stopwatch == false)
            {
                full_second = 0;
                dispatcherTimer.Start(); // запускаем тацймер
                dispatcherTimerInterval.Start();
                start_stopwatch = true; // запоминаем состояние
                start.Content = "Стоп"; // меняем надпись на кнопке
            }
            else
            { // если тайм запущен
                dispatcherTimer.Stop(); // отсанавливаем таймер
                dispatcherTimerInterval.Stop();
                start_stopwatch = false;
                start.Content = "Начать";
            }
        }

        private void Interval(object sender, RoutedEventArgs e)
        {
            if (start_stopwatch == false) return;
            dispatcherTimerInterval.Stop();
            full_secondInterval = 0;
            times.Items.Add(MyTime);
            dispatcherTimerInterval.Start();
        }

        private void Starttimer(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
            mainwindow.OpenPages(MainWindow.pages.timer);
        }
    }
}
