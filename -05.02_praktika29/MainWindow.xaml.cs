using System.Windows;

namespace praktika29
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            this.DataContext = new ViewModels.VMPages();
        }
    }
}