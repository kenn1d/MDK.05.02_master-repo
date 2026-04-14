using System.Windows;
using System.Windows.Controls;
using praktika26.Classes;

namespace praktika26.Pages.Clubs
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public ClubContext AllClub = new ClubContext();
        public Main()
        {
            InitializeComponent();
            foreach (Models.Clubs Club in AllClub.Clubs)
                Parent.Children.Add(new Elements.Item(Club, this));
        }

        private void AddClub(object sender, System.Windows.RoutedEventArgs e) {
            if (Classes.ActiveUser.User.Login == "admin")
            {
                MainWindow.init.OpenPages(new Pages.Clubs.Add(this));
            }
            else MessageBox.Show("Откзано в доступе", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SortName(object sender, System.Windows.RoutedEventArgs e) =>
            Search("Name");

        private void SortAddress(object sender, RoutedEventArgs e) =>
            Search("Address");

        private void SortTime(object sender, RoutedEventArgs e) =>
            Search("Time");

        public void Search(string type)
        {
            if (SearchParam.Text != "")
            {
                IEnumerable<Models.Clubs> SearchClubs = AllClub.Clubs;
                IEnumerable<Models.Clubs> Result = SearchClubs;
                if (type == "Name") Result = SearchClubs.Where(x => x.Name.ToLower().Contains(SearchParam.Text.ToLower()));
                else if (type == "Address") Result = SearchClubs.Where(x => x.Address.ToLower().Contains(SearchParam.Text.ToLower()));
                else Result = SearchClubs.Where(x => x.WorkTime.ToLower().Contains(SearchParam.Text.ToLower()));

                if (Result.Count() > 0)
                {
                    Parent.Children.Clear();
                    foreach (var Club in Result)
                        Parent.Children.Add(new Elements.Item(Club, this));
                }
                else MessageBox.Show("Совпадений не найдено", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else MessageBox.Show("Поле запроса было пустым. Введите текст для сортировки", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
