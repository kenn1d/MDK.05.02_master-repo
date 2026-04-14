using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace Chess_Kibanov.Classes
{
    public class Pawn
    {
        ///<summary> Координата X
        public int X {  get; set; }

        ///<summary> Координата Y
        public int Y { get; set; }

        ///<summary> Координата отвечающая за то, выбрна ли фигура для перемещения или нет
        public bool Select = false;

        ///<summary> Координата отвечающая за то, к какой стороне относится фигура Белая или черная
        public bool Black = false;

        ///<summary> Ссылка на созданную фигуру на интерфейсе
        public Grid Figure { get; set; }

        ///<summary> Конструктор класса Pawn
        public Pawn (int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }


        ///<summary> Метод выбора фигуры
        public void SelectFigure(object sender, EventArgs e)
        {
            // Переменная отвечающая за то, атакуют ли нашу фигуру
            bool atack = false;
            // Среди всех наших пешек ищем пешку, которая является выделенной
            Pawn SelectPawn = MainWindow.mainWindow.Pawns.Find(x => x.Select == true);

            // Если выделенная пешка существует
            if (SelectPawn != null)
            {
                // Проверяем атакует ли нас эта пешка, если это чёрная пешка, проверяем находится ли она ниже нас, и входит ли в диапазон атаки
                if(this.Black && this.Y - 1 == SelectPawn.Y && (this.X - 1 == SelectPawn.X || this.X + 1 == SelectPawn.X) ||
                    // Если пешка является чёрной, проверяем находится ли пешка ниже нас, и входит ли в диапазон атаки
                    !this.Black && this.Y + 1 == SelectPawn.Y && (this.X - 1 == SelectPawn.X || this.X + 1 == SelectPawn.X))
                {
                    // Обращаемся к доске и удляем пешку с доски
                    MainWindow.mainWindow.gameBoard.Children.Remove(this.Figure);
                    // Перемещаем атакующую пешку на координаты атакованой пешки
                    Grid.SetColumn(SelectPawn.Figure, this.X);
                    Grid.SetRow(SelectPawn.Figure, this.Y);
                    // Присваиваем обновлённые координаты пешке
                    SelectPawn.X = this.X;
                    SelectPawn.Y = this.Y;
                    // Вызываем выделение пешки, которое снимет с неё выделение
                    SelectPawn.SelectFigure(null, null);
                    // Запоминаем что была произведена атака
                    atack = true;
                }
            }

            // Если атака не была произведена,значит нам неоходимо выделить пешку
            if(!atack)
            {
                // Вызываем метод снятия выделения со всех наших пешек, которые находятся на доске
                MainWindow.mainWindow.OnSelectPawn(this);
                // Если мы уже были выделены
                if (this.Select)
                {
                    // В зависимости от нашего цвета Белого/Черного, отображаем иконку
                    if (this.Black)
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (black).png")));
                    else
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));

                    // Запоминаем что пешка более не является выделенной
                    this.Select = false;
                }
                else
                {
                    // Если же пешка не выделена, изменяем иконку на выделенную
                    this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (select).png")));
                    // Запоминаем что пешка является выделенной
                    this.Select = true;

                    // Передаём данные в метод подсветки
                    MainWindow.mainWindow.ResetColor();
                    MainWindow.mainWindow.ShowMove(this.X, this.Y, "Pawn", this.Black);
                }
            }
        }

        ///<summary> Метод перемещения пешки
        public void Transform(int X, int Y)
        {
            // Начнём с того, что пешка может перемещаться только прямо
            // Если координата X не совпадает с нашей координатой, то
            if (X != this.X)
            {
                // Снимаем выделение пешки
                SelectFigure(null, null);
                // Заканчиваем выполнение метода
                return;
            }

            // Проверяем коодинату по Y
            // Если пешка является черной, и если клетка на которой она стоит 1, и пользователь хочет переместить нашу фигуру на 2 клетку или на 1 клетку
            if (!Black && ((this.Y == 1 && this.Y + 2 == Y) || this.Y + 1 == Y) ||
                // Если пешка является белой, и если клетка на которой она стоит 1,
                // и пользователь хочет переместить нашу фигуру на 2 клетки или на 1 клетку
                Black && ((this.Y == 6 && this.Y - 2 == Y) || this.Y - 1 == Y))
            {
                // Изменяем положение фигуры на доске по X и Y
                Grid.SetColumn(this.Figure, X);
                Grid.SetRow(this.Figure, Y);
                // Запоминаем координаты на которые переместили пешку
                this.X = X;
                this.Y = Y;
            }

            MainWindow.mainWindow.ResetColor();

            // Снимаем выделение с пешки
            SelectFigure(null, null);
        }
    }
}
