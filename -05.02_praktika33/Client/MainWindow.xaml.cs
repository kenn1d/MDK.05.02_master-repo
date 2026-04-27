using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Сатическая ссылка на главное окно
        /// Используется для доступа к методам нафигации из любого места приложения
        /// Паттерн "Одиночка" (Singleton) для простого доступа к главному окну
        /// </summary>
        public static MainWindow init;
        /// <summary>
        /// Статическое поле для хранения JWT Токена аутентификации
        /// Доступно из любой точки приложения после успешного входа
        /// </summary>
        public static string Token;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(new Pages.Login());
        }

        /// <summary>
        /// Метод навигации между страницами
        /// Отображает переданную страницу внутри фрейма frame
        /// </summary>
        /// <param name="openPage">Страница для отображения (Login, Main, Add)</param>
        public void OpenPages(Page openPage)
        {
            frame.Navigate(openPage);
        }
    }
}