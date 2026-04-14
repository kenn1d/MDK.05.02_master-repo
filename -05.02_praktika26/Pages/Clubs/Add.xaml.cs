using System.Windows;
using System.Windows.Controls;

namespace praktika26.Pages.Clubs
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Main Main;
        Models.Clubs Club;

        public Add(Main main, Models.Clubs club = null)
        {
            InitializeComponent();

            this.Main = main;
            Club = club;

            // Если пришёл клуб на изменение
            if(Club != null)
            {
                this.Club = club;
                this.Name.Text = Club.Name;
                this.Address.Text = Club.Address;
                this.WorkTime.Text = Club.WorkTime;
                BthAdd.Content = "Изменить";
            }
        }

        private void AddClub(object sender, RoutedEventArgs e)
        {
            if (Club == null)
            {
                Club = new Models.Clubs();
                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;
                // Добавляем объект в контекст
                this.Main.AllClub.Clubs.Add(this.Club);
            }
            else
            {
                // Если изменение
                // Изменяем данные
                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;
            }
            this.Main.AllClub.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }
    }
}
