using System.Windows.Controls;
using System.Windows.Media;

namespace praktika6.Classes
{
    public class Ellipse : Figure
    {
        public double Diametr;

        public Ellipse(int X, int Y, double Diametr) : base(X, Y)
        {
            this.Diametr = Diametr;
        }

        public void Draw(Canvas canvas, int x, int y, double d)
        {
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Width = d;
            ellipse.Height = d;
            ellipse.Fill = Brushes.Black;
            Canvas.SetLeft(ellipse, x - d / 2);
            Canvas.SetTop(ellipse, y - d / 2);
            canvas.Children.Add(ellipse);
        }
    }
}
