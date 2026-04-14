using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace praktika6.Classes
{
    public class Triangle : Figure
    {
        public double FirstBord;
        public double SecBord;
        public double LastBord;
        public int Check = 0;

        public Triangle(int X, int Y, double FirstBord, double SecBord, double LastBord) : base(X, Y)
        {
            this.FirstBord = FirstBord;
            this.SecBord = SecBord;
            this.LastBord = LastBord;
        }

        public void Draw(Canvas canvas, int x, int y, double FirstBord, double SecBord, double LastBord)
        {
            System.Windows.Shapes.Polygon triangle = new System.Windows.Shapes.Polygon();
            triangle.Fill = Brushes.Aquamarine;
            PointCollection points = CalculatePoint(FirstBord, SecBord, LastBord, x, y);
            triangle.Points = points;
            if (Check == 1)
            {
                Canvas.SetLeft(triangle, x - FirstBord / 3); //  - FirstBord / 3
                Canvas.SetTop(triangle, y - LastBord / 2); //- LastBord / 2
            }
            canvas.Children.Add(triangle);
        }

        private PointCollection CalculatePoint(double FirstBord, double SecBord, double LastBord, double x, double y)
        {
            PointCollection points = new PointCollection();

            if (SecBord == 0 && LastBord == 0)
            {
                double height = FirstBord * Math.Sqrt(3) / 2; // высота равностороннего треугольника

                Point p1 = new Point(x, y + height / 2); // 
                Point p2 = new Point(x + FirstBord / 2, y - height / 2); //првая точка
                Point p3 = new Point(x - FirstBord / 2, y - height / 2); //левая точка

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else
            {
                Point p1 = new Point(0, 0); // 
                Point p2 = new Point(0, LastBord);
                Point p3 = new Point(0 + FirstBord, LastBord);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);

                Check = 1;
            }
            return points;
        }
    }
}
