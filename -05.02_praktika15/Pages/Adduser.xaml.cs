using praktika15.Classes;
using System.Windows;
using System.Windows.Controls;

namespace praktika15.Pages
{
    /// <summary>
    /// Логика взаимодействия для Adduser.xaml
    /// </summary>
    public partial class Adduser : Page
    {
        UserContext User;

        public Adduser(UserContext userContext = null)
        {
            InitializeComponent();
            if (userContext != null)
            {
                User = userContext;
                tbName.Text = userContext.Name;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.ViewUsers());
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length == 0)
            {
                MessageBox.Show("Необходимо указать наименование");
                return;
            }

            if (User == null)
            {
                User = new UserContext()
                {
                    Name = tbName.Text
                };
                User.Save();
                MessageBox.Show("Документ добавлен");
            }
            else
            {
                User.Name = tbName.Text;
                User.Save(true);
                MessageBox.Show("Документ изменён");
            }
            MainWindow.init.AllUsers = new UserContext().AllUsers();
            MainWindow.init.frame.Navigate(new ViewUsers());
        }
    }
}
