using praktika15.Classes;
using System.Windows.Controls;

namespace praktika15.Elements
{
    /// <summary>
    /// Логика взаимодействия для ItemUser.xaml
    /// </summary>
    public partial class ItemUser : UserControl
    {
        UserContext User;

        public ItemUser(UserContext userContext)
        {
            InitializeComponent();
            
            lUser.Content = "Ответственный: " + userContext.Name;

            this.User = userContext;
        }

        private void EditDocument(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.frame.Navigate(new Pages.Adduser(User));

        private void DeleteDocument(object sender, System.Windows.RoutedEventArgs e)
        {
            User.Delete();
            MainWindow.init.AllUsers = new UserContext().AllUsers();
            MainWindow.init.frame.Navigate(new Pages.ViewUsers());
        }
    }
}
