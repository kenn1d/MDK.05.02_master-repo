using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace praktika11.Pages
{
    /// <summary>
    /// Логика взаимодействия для Timer.xaml
    /// </summary>
    public partial class Timer : Page
    {
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public float full_second = 0;
        public bool start_timer = false;

        public Timer()
        {
            InitializeComponent();

            dispatcherTimer.Tick += TimerSecond;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// Функция таймера для выполнения
        /// </summary>
        private void TimerSecond(object sender, EventArgs e)
        {
            if (full_second <= 0)
            {
                dispatcherTimer.Stop();
                full_second = 0;
                start_timer = false;
                start.Content = "Начать";
                time.Content = "00:00:00";
                return;
            }

            full_second--;

            float hours = (int)(full_second / 60 / 60);
            float minuts = (int)(full_second / 60) - (hours * 60);
            float seconds = full_second - (hours * 60 * 60) - (minuts * 60);
            time.Content = $"{hours:00}:{minuts:00}:{seconds:00}";
        }

        private void StartTimer(object sender, RoutedEventArgs e)
        {
            if (start_timer == false)
            {
                if (full_second == 0)
                {
                    if (int.TryParse(hours.Text, out int hour))
                        if (int.TryParse(minuts.Text, out int minut))
                            if (int.TryParse(seconds.Text, out int second))
                                full_second = hour * 3600 + minut * 60 + second;
                            else { MessageBox.Show("Проверьте правильность ввода секунд"); return; }
                        else { MessageBox.Show("Проверьте правильность ввода минут"); return; }
                    else { MessageBox.Show("Проверьте правильность ввода часов"); return; }
                }
                dispatcherTimer.Start();
                start_timer = true;
                start.Content = "Стоп";
            }
            else
            {
                dispatcherTimer.Stop();
                start_timer = false;
                start.Content = "Продолжить";
            }
        }

        private void Startwatch(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
            mainwindow.OpenPages(MainWindow.pages.stopwatch);
        }

        private void _return(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            start_timer = false;
            start.Content = "Начать";
            full_second = 0;
            hours.Text = "0";
            minuts.Text = "0";
            seconds.Text = "0";
            time.Content = "00:00:00";
            start.Content = "Начать";
        }
    }
}
