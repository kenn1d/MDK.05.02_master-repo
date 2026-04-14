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

namespace praktika6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Classes.Ellipse> Ellipses = new List<Classes.Ellipse>();
        public List<Classes.Triangle> Triangles = new List<Classes.Triangle>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void create_ellipse(object sender, RoutedEventArgs e)
        {
            Ellipses.Add(new Classes.Ellipse(int.Parse(el_X.Text), int.Parse(el_Y.Text), double.Parse(el_D.Text)));
            foreach(var i in Ellipses) 
                i.Draw(Figures, i.X, i.Y, i.Diametr);
        }

        private void create_triangle(object sender, RoutedEventArgs e)
        {
            Triangles.Add(new Classes.Triangle(int.Parse(tr_X.Text), int.Parse(tr_Y.Text), int.Parse(tr_st1.Text), int.Parse(tr_st2.Text), int.Parse(tr_st3.Text)));
            foreach (var i in Triangles)
                i.Draw(Figures, i.X, i.Y, i.FirstBord, i.SecBord, i.LastBord);
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            Figures.Children.Clear();
            Triangles.Clear();
            Ellipses.Clear();
        }
    }
}
