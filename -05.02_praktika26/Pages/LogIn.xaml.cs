using System.Windows;
using System.Windows.Controls;
using praktika26.Classes;

namespace praktika26.Pages
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public UserContext AllUsers = new UserContext();

        public LogIn()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            var user = AllUsers.Users.FirstOrDefault(x => x.Login == Login.Text && x.Password == Password.Text);
            if (user != null)
            {
                ActiveUser.User = user;
                MainWindow.init.BthClubs.IsEnabled = true;
                MainWindow.init.BthUsers.IsEnabled = true;
                MainWindow.init.OpenPages(new Pages.Clubs.Main());
            }
            else if (Login.Text == "admin" && Password.Text == "admin")
            {
                Models.Users Admin = new Models.Users() { 
                    FIO = null,
                    RentStart = DateTime.Today,
                    Duration = 0,
                    Login = Login.Text,
                    Password = Password.Text
                };
                ActiveUser.User = Admin;
                MainWindow.init.BthClubs.IsEnabled = true;
                MainWindow.init.BthUsers.IsEnabled = true;
                MainWindow.init.OpenPages(new Pages.Clubs.Main());
            }
            else MessageBox.Show("Пользователь не найден", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
