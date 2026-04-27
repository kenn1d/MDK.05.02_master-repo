using System.Windows;
using System.Windows.Controls;
using Client.Context;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Асинхронный метод аутентификации пользователя
        /// Отправляет логин и пароль на сервер и получает токен
        /// </summary>
        /// <param name="login">Логин пользователя (введёный в текстовое поле)</param>
        /// <param name="password">Пароль пользователя (введёный в текстовое поле)</param>
        /// <returns></returns>
        public async Task Auth(string login, string password)
        {
            // Вызов статического метода Login из класса UserContext
            // Отправляет запрос на сервер и ожидает ответ
            string? Token = await UserContext.Login(login, password);
            // Проверка успешности авторизации
            if (Token == null)
            {
                // Если токен не получен - показываем сообщение об ошибке
                MessageBox.Show("Логин и пароль указаны не верно");
            }
            else
            {
                // Сохраняем полученный токен в статическом поле MainWindow
                // Токен будет использоваться для всех последующих запросов к API
                MainWindow.Token = Token;
                // Открываем главную страницу приложения
                // OpenPages - метод для навигации между страницами
                MainWindow.init.OpenPages(new Pages.Main());
            }
        }

        private void BthAuth(object sender, RoutedEventArgs e) { 
            // Валидация поля логина
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Необходимо указать логин пользователя");
                return; // Прерываем выполнение метода
            }

            // Валидация поля пароля
            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Необходимо указать пароль пользователя");
                return; // Прерываем выполнение метода
            }

            // Если все поля заполнены - вызываем  метод авторизации
            // Передаем введенные логин и пароль
            Auth(tbLogin.Text, tbPassword.Password);
        }

        private void toReg(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Regin());
        }
    }
}
