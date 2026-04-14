using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace praktika12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new Pages.Parents());
        }

        public enum pages
        {
            statement,
            education,
            status,
            speciality,
            passport,
            contacts,
            parents
        }

        public void Openpage(pages page)
        {
            if (page == pages.statement) frame.Navigate(new Pages.Statement());
            if (page == pages.education) frame.Navigate(new Pages.Education());
            if (page == pages.status) frame.Navigate(new Pages.Status());
            if (page == pages.speciality) frame.Navigate(new Pages.Speciality());
            if (page == pages.passport) frame.Navigate(new Pages.Passport());
            if (page == pages.contacts) frame.Navigate(new Pages.Contacts());
            if (page == pages.parents) frame.Navigate(new Pages.Parents());
        }
    }
}
