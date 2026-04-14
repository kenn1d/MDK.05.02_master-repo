using praktika26.Classes;
using System.Windows;
using System.Windows.Controls;

namespace praktika26.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public ClubContext AllClub = new ClubContext();
        public UserContext AllUsers = new UserContext();

        public Main()
        {
            InitializeComponent();

            foreach (var user in AllUsers.Users)
                Parent.Children.Add(new Elements.Item(user, this));

            SearchClub.Items.Add("Выберите клуб ...");
            SearchClub.SelectedIndex = 0;
            foreach (var Club in AllClub.Clubs)
                SearchClub.Items.Add($"{Club.Name}");
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (Classes.ActiveUser.User.Login == "admin") {
                MainWindow.init.OpenPages(new Pages.Users.Add(this));
            } else MessageBox.Show("Откзано в доступе", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error) ;
        }

        private void SortFIO(object sender, RoutedEventArgs e) =>
            Search("fio");

        private void SortRentStart(object sender, RoutedEventArgs e) =>
            Search("rent");

        private void SortTime(object sender, RoutedEventArgs e) =>
            Search("time");

        private void SelectClub(object sender, RoutedEventArgs e)
        {
            if (SearchClub.SelectedIndex > 0)
            {
                var SelectedClub = AllClub.Clubs.First(x => x.Name == SearchClub.SelectedItem.ToString());

                if (SelectedClub != null)
                {
                    var Results = AllUsers.Users.Where(x => x.IdClub == SelectedClub.Id);

                    if (Results.Any())
                    {
                        Parent.Children.Clear();
                        foreach (var User in Results)
                            Parent.Children.Add(new Elements.Item(User, this));
                    }
                    else
                    {
                        MessageBox.Show("Совпадений не найдено", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        public void Search(string type)
        {
            if (SearchParam.Text != "")
            {
                IEnumerable<Models.Users> SearchUsers = AllUsers.Users;
                IEnumerable<Models.Users> Result = SearchUsers;
                if (type == "fio") Result = SearchUsers.Where(x => x.FIO.ToLower().Contains(SearchParam.Text.ToLower()));
                else if (type == "rent") Result = SearchUsers.Where(x => x.RentStart.ToString().Contains(SearchParam.Text));
                else Result = SearchUsers.Where(x => x.Duration.ToString().Contains(SearchParam.Text));

                if (Result.Count() > 0)
                {
                    Parent.Children.Clear();
                    foreach (var User in Result)
                        Parent.Children.Add(new Elements.Item(User, this));
                }
                else MessageBox.Show("Совпадений не найдено", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else MessageBox.Show("Поле запроса было пустым. Введите текст для сортировки", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
