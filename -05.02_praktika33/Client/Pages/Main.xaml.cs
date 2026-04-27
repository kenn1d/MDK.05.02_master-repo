using Client.Context;
using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            GetStorage();
        }

        /// <summary>
        /// Асинхронный метод получения списка хранилищ с сервера
        /// Отправляет GET запрос через StorageContext и отображает элементы на странице
        /// </summary>
        public async Task GetStorage()
        {
            // Получаем список хранилищ с сервера
            // GET /storage/get с токеном в заголовке
            List<Storage> Storages = await StorageContext.Get();
            // Очищаем контейнер перед обновлением новых элементов
            StorageList.Children.Clear();
            // Для каждого хранилаща создаем визуальный элемент Item
            foreach (Storage Storage in Storages) { 
                // Добавляем элемент в контейнер
                // Item - пользовательский элемент управления, отображающий одно хранилище
                StorageList.Children.Add(new Elements.Item(Storage, this));
            }
        }

        /// <summary>
        /// Обработчик нажатиякнопки добавления нового хранилища
        /// Открывает страницу добавления
        /// </summary>
        private void OpenPageAdd(object sender, RoutedEventArgs e)
        {
            // Открываем страницу доавбления нового хранилища
            // Используется метод навигации из главного окна приложения
            MainWindow.init.OpenPages(new Pages.Add(null));
        }
    }
}
