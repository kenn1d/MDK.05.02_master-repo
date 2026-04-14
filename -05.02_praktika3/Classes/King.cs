using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Chess_Kibanov.Classes
{
    public class King
    {
        
        public int X { get; set; }
        public int Y { get; set; }
        public bool Select = false;
        public bool Black = false;
        public Grid Figure { get; set; }
        public King (int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }

        public void SelectFigure(object sender, EventArgs e)
        {
            bool atack = false;
            King SelectKing = MainWindow.mainWindow.Kings.Find(x => x.Select == true);
            if (SelectKing != null)
            {
                if ((this.Black || !this.Black) && (this.Y - 1 == SelectKing.Y || this.Y + 1 == SelectKing.Y) && (this.X - 1 == SelectKing.X || this.X + 1 == SelectKing.X) )
                {
                    MainWindow.mainWindow.gameBoard.Children.Remove(this.Figure);
                    Grid.SetColumn(SelectKing.Figure, this.X);
                    Grid.SetRow(SelectKing.Figure, this.Y);
                    SelectKing.X = this.X;
                    SelectKing.Y = this.Y;
                    SelectKing.SelectFigure(null, null);
                    atack = true;
                }
            }

            if (!atack)
            {
                MainWindow.mainWindow.OnSelectKing(this);
                if (this.Select)
                {
                    if (this.Black)
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/king (black).png")));
                    else
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/king (white).png")));

                    this.Select = false;
                }
                else
                {
                    this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/king (select).png")));
                    this.Select = true;

                    // Передаём данные в метод подсветки
                    MainWindow.mainWindow.ShowMove(this.X, this.Y, "King", this.Black);
                }
            }
        }

        ///<summary> Метод перемещения короля
        public void Transform(int X, int Y)
        {
            if ((!Black || Black) && ((this.X + 1 == X || this.X - 1 == X || this.X == X) && (this.Y + 1 == Y || this.Y - 1 == Y || this.Y == Y)))
            {
                Grid.SetColumn(this.Figure, X);
                Grid.SetRow(this.Figure, Y);

                this.X = X;
                this.Y = Y;
            }
            MainWindow.mainWindow.ResetColor();
            SelectFigure(null, null);
        }
    }
}
