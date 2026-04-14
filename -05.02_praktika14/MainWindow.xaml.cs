using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace praktika14
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenPage(pages.Categories);
        }

        public enum pages
        {
            Categories,
            Main
        }

        ///<summary>
        /// Функция лькрытия страниц
        /// </summary>
        /// <param name="page">страница которую необходимо открыть</param>
        public void OpenPage(pages page, List<Classes.Item> categoryItems)
        {
            if (page == pages.Categories) frame.Navigate(new Pages.Categories());
            else if (page == pages.Main) frame.Navigate(new Pages.Main(categoryItems));
        }

        public void OpenPage(pages page)
        {
            if (page == pages.Categories) frame.Navigate(new Pages.Categories());
        }
    }
}
