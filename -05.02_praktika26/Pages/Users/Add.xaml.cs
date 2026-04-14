using praktika26.Classes;
using System.Windows.Controls;

namespace praktika26.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public ClubContext AllClub = new ClubContext();
        Main Main;
        Models.Users User;

        public Add(Main main, Models.Users user = null)
        {
            InitializeComponent();
            
            Clubs.Items.Add("Выберите...");

            foreach (var club in AllClub.Clubs)
                Clubs.Items.Add(club.Name);

            this.Main = main;
            this.User = user;
            if(user != null ) {
                this.User = user;
                this.FIO.Text = user.FIO;
                this.RentStart.Text = user.RentStart.ToString("yyyy-MM-dd");
                this.RentTime.Text = user.RentStart.ToString("HH:mm");
                this.Duration.Text = user.Duration.ToString();
                Clubs.SelectedItem = AllClub.Clubs.Where(x => x.Id == user.IdClub).First().Name;
                BthAdd.Content = "Изменить";
            }


        }

        private void AddUser(object sender, System.Windows.RoutedEventArgs e)
        {
            // Создаём дату аренды
            DateTime DTRentStart = new DateTime();
            // Конвертируем дату
            DateTime.TryParse(this.RentStart.Text, out DTRentStart);
            // Добавляем время с поля
            DTRentStart = DTRentStart.Add(TimeSpan.Parse(this.RentTime.Text));
            // Если пользователь для добавления
            if (this.User == null)
            {
                User = new Models.Users();
                User.FIO = this.FIO.Text;
                User.RentStart = DTRentStart;
                User.Duration = Convert.ToInt32(this.Duration.Text);
                User.IdClub = AllClub.Clubs.Where(x => x.Name == Clubs.SelectedItem).First().Id;
                this.Main.AllUsers.Users.Add(this.User);
            }
            else
            {
                User.FIO = this.FIO.Text;
                User.RentStart = DTRentStart;
                User.Duration = Convert.ToInt32(this.Duration.Text);
                User.IdClub = AllClub.Clubs.Where(x => x.Name == Clubs.SelectedItem).First().Id;
            }
            // Сохраняем все изменения
            this.Main.AllUsers.SaveChanges();
            // Открываем страницу с пользователями
            MainWindow.init.OpenPages(new Main());
        }
    }
}
