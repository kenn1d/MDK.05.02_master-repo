using Client.Context;
using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        /// <summary>
        /// хранит редактируемую запись (если не null - режим редактирования)
        /// </summary>
        Storage ChangeStorage;

        public Add(Storage storage = null)
        {
            InitializeComponent();
            ChangeStorage = storage;
            if (ChangeStorage != null)
            {
                tbName.Text = ChangeStorage.Name;
                tbUrl.Text = ChangeStorage.Url;
                tbLogin.Text = ChangeStorage.Login;
                tbPassword.Text = ChangeStorage.Password;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранить
        /// </summary>
        private void Save(object sender, RoutedEventArgs e)
        {
            // Режим 1: Создание новой записи
            if (ChangeStorage == null)
            {
                // Создаём новый объект Storage с данными из полей ввода
                Storage storage = new Storage()
                {
                    Name = tbName.Text,
                    Url = tbUrl.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text
                };

                // Отправляем POST запрос на сервер для создания новой записи
                StorageContext.Add(storage);
            }
            // Режим 2: Редактирование существующей записи
            else
            {
                // Обновляем поля существующего объекта данными из формы
                ChangeStorage.Name = tbName.Text;
                ChangeStorage.Url = tbUrl.Text;
                ChangeStorage.Login = tbLogin.Text;
                ChangeStorage.Password = tbPassword.Text;

                // Отправляем PUT Зарос на сервер для обновления записи
                StorageContext.Update(ChangeStorage);
            }
            // Показываем сообщение об успешном сохранении
            MessageBox.Show("Данные сохранены");
            // Возвращаемся на главную страницу
            MainWindow.init.OpenPages(new Pages.Main());
        }

        /// <summary>
        /// Обработчик для кнопки Назад
        /// </summary>
        private void Back(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Main());
    }
}
