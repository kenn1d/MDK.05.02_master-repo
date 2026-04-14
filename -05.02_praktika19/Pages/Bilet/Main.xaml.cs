using System.Collections.Generic;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.Bilet
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<BiletContext> AllBilets = BiletContext.Select();

        public Main()
        {
            InitializeComponent();

            foreach (BiletContext item in AllBilets)
            {
                parent.Children.Add(new Items.Item(item));
            }
        }
    }
}
