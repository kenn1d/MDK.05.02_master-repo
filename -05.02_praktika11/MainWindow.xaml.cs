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

namespace praktika11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenPages(pages.stopwatch); // открываем страницу секундомера
        }

        ///<summary>
        ///перечисляемый тип сраниц
        ///</summary>
        public enum pages
        {
            stopwatch, // секундомер
            timer
        }

        /// <summary>
        /// Функция открытия страниц
        /// </summary>
        /// <param name="_page">Открыть страницу</param>
        public void OpenPages(pages _page)
        {
            if(_page == pages.stopwatch) // если открытая страница является секундомером
                frame.Navigate(new Pages.Stopwatch()); // открываем страницу секундомера
            if(_page == pages.timer)
                frame.Navigate(new Pages.Timer());
        }
    }
}
