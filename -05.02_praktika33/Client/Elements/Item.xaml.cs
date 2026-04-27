using Client.Context;
using Client.Models;
using Client.Pages;
using System.Windows;
using System.Windows.Controls;

namespace Client.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Storage Storage;
        Main Main;

        /// <summary>
        /// Конструктор элемента
        /// </summary>
        /// <param name="storage">Данные для отображения</param>
        /// <param name="main">Ссылка на главную страницу</param>
        public Item(Storage storage, Main main)
        {
            InitializeComponent();
            tbName.Text = storage.Name;
            tbUrl.Text = storage.Url;
            tbLogin.Text = storage.Login;
            tbPassword.Text = storage.Password;

            this.Main = main;
            this.Storage = storage;
        }

        /// <summary>
        /// Обработчик кнопки Редактировать
        /// </summary>
        private void Update(object sender, RoutedEventArgs e)
        {
            // Передаём текущий объект Storage для редактирования
            MainWindow.init.OpenPages(new Pages.Add(Storage));
        }

        /// <summary>
        /// Обработчик кнопки Удалить
        /// </summary>
        private void Delete(object sender, RoutedEventArgs e)
        {
            // Отправляем DELETE зарос на сервер
            StorageContext.Delete(Storage.Id);
            // Удаляем элемент из визуального списка на глвной странице
            this.Main.StorageList.Children.Remove(this);
            // Показываем сообщение об успешном удалении
            MessageBox.Show("Данные удалены");
        }
    }
}
