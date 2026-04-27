using Client.Context;
using System.Windows;
using System.Windows.Controls;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        public Regin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Асинхронный метод регистрации пользователя
        /// </summary>
        /// <param name="login">Логин подльзователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>True или false в зависимости от успешности</returns>
        public async Task Reg(string login, string password)
        {
            bool status = await UserContext.Regin(login, password);
            if (status)
            {
                MessageBox.Show("Регистрация прошла успешно");
                MainWindow.init.OpenPages(new Pages.Login());
            }
        }

        private void BthReg(object sender, RoutedEventArgs e)
        {
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
            Reg(tbLogin.Text, tbPassword.Password);
        }

        private void toAuth(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Login());
        }
    }
}
