using System.Windows;
using System.Windows.Controls;

namespace praktika26.Pages.Clubs.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        /// <summary> Главная страница клубов
        Main Main;
        /// <summary> Данные клуба
        Models.Clubs Club;

        public Item(Models.Clubs Club, Main Main)
        {
            InitializeComponent();

            this.Name.Text = Club.Name;
            this.Address.Text = Club.Address;
            this.WorkTime.Text = Club.WorkTime;

            this.Main = Main;
            this.Club = Club;
        }

        /// <summary> Метод изменения
        private void EditClub(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Classes.ActiveUser.User.Login == "admin")
            {
                MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));
            }
            else MessageBox.Show("Откзано в доступе", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary> Метод удаления
        private void DeleteClub(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Classes.ActiveUser.User.Login == "admin")
            {
                // Удаляем клуб из контекста
                Main.AllClub.Clubs.Remove(this.Club);
                // Сохраняем изменения
                Main.AllClub.SaveChanges();
                // Удаляем элемент со страницы Main
                Main.Parent.Children.Remove(this);
            }
            else MessageBox.Show("Откзано в доступе", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
