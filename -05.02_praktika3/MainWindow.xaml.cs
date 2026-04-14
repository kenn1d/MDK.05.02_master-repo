using Chess_Kibanov.Classes;
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

namespace Chess_Kibanov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ///<summary> Статичная переменная  для обращения к MainWindow
        public static MainWindow mainWindow;

        // TODO: Коллекция гридов
        public List<Classes.Grides> Grids = new List<Classes.Grides>();
        public Classes.Grides GridosCoordinatos(int X, int Y)
        {
            return Grids.Find(i => i.tileX == X && i.tileY == Y);
        }

        ///<summary> Коллекция пешек
        public List<Classes.Pawn> Pawns = new List<Classes.Pawn>();
        ///<summary> Коллекция королей
        public List<Classes.King> Kings = new List<Classes.King>();

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.mainWindow = this;

            // TODO: Добавляем гриды в коллекцию
            foreach(object element in gameBoard.Children)
            {
                Grid Tile = element as Grid;
                Grids.Add(new Classes.Grides(Grid.GetColumn(Tile), Grid.GetRow(Tile), Tile));
            }

            // Добавлеие белых фигур
            Kings.Add(new Classes.King(4, 0, false));
            Pawns.Add(new Classes.Pawn(0, 1, false));
            Pawns.Add(new Classes.Pawn(1, 1, false));
            Pawns.Add(new Classes.Pawn(2, 1, false));
            Pawns.Add(new Classes.Pawn(3, 1, false));
            Pawns.Add(new Classes.Pawn(4, 1, false));
            Pawns.Add(new Classes.Pawn(5, 1, false));
            Pawns.Add(new Classes.Pawn(6, 1, false));
            Pawns.Add(new Classes.Pawn(7, 1, false));

            // Добавление чёрных фигур
            Kings.Add(new Classes.King(4, 7, true));
            Pawns.Add(new Classes.Pawn(0, 6, true));
            Pawns.Add(new Classes.Pawn(1, 6, true));
            Pawns.Add(new Classes.Pawn(2, 6, true));
            Pawns.Add(new Classes.Pawn(3, 6, true));
            Pawns.Add(new Classes.Pawn(4, 6, true));
            Pawns.Add(new Classes.Pawn(5, 6, true));
            Pawns.Add(new Classes.Pawn(6, 6, true));
            Pawns.Add(new Classes.Pawn(7, 6, true));

            CreateFigure();
        }

        /// <summary> Метод отключения выбраннх тайлов
        public void OnSelectPawn(Classes.Pawn SelectPawn)
        {
            // Перебираем коллекцию пешек
            foreach (Classes.Pawn Pawn in Pawns)
                // Если перебираемая пешка является не действительно выбранной
                if (Pawn != SelectPawn)
                    // Если пешка не выбрана
                    if (Pawn.Select)
                        // Вызываем метод выбора, который отключит выбор пешки
                        Pawn.SelectFigure(null, null);
        }
        public void OnSelectKing(Classes.King SelectKing)
        {
            // Перебираем коллекцию королей
            foreach (Classes.King King in Kings)
                // Если перебираемый король является не действительно выбранной фигурой
                if (King != SelectKing)
                    // Если король не выбран
                    if (King.Select)
                        // Вызываем метод выбора, который отключит выбор короля
                        King.SelectFigure(null, null);
        }

        /// <summary> Метод выбора тайла
        public void SelectTile(object sender, MouseButtonEventArgs e)
        {
            // Преобразовываем выбранный элемент в Grid
            Grid Tile = sender as Grid;
            // Получаем координаты выбранного тайла
            int X = Grid.GetColumn(Tile);
            int Y = Grid.GetRow(Tile);

            // Получаем выбранную пешку
            Classes.Pawn SelectPawn = Pawns.Find(x => x.Select == true);
            // Если выбранная пешка присутствует
            if (SelectPawn != null)
            {
                // Перемещем пешку на выбранный тайл
                SelectPawn.Transform(X, Y);
            }

            Classes.King SelectKing = Kings.Find(x => x.Select == true);
            if (SelectKing != null)
            {
                SelectKing.Transform(X, Y);
            }
        }

        /// <summary> Метод создания фигур
        public void CreateFigure()
        {
            // Перебираем коллекцию пешек
            foreach (Classes.Pawn Pawn in Pawns)
            {
                // Создаём элемент Grid, с размерами тайла
                Pawn.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };
                // В зависимости от цвета пешки, указываем ей изображение
                if (Pawn.Black)
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (black).png")));
                else
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));
                // Перемещаем пешку на указанную позицию/стартовую позицию
                Grid.SetColumn(Pawn.Figure, Pawn.X);
                Grid.SetRow(Pawn.Figure, Pawn.Y);
                // TODO: Подписываемся на событие нажатия на пешку
                Pawn.Figure.MouseDown += Pawn.SelectFigure;
                // Добавляем на интерфейс созданую пешку
                gameBoard.Children.Add(Pawn.Figure);
            }

            foreach (Classes.King King in Kings)
            {
                King.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };
                if (King.Black)
                    King.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/king (black).png")));
                else
                    King.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/king (white).png")));
                Grid.SetColumn(King.Figure, King.X);
                Grid.SetRow(King.Figure, King.Y);
                King.Figure.MouseDown += King.SelectFigure;
                gameBoard.Children.Add(King.Figure);
            }
        }

        /// <summary> Метод создания подсветки ходов
        public void ShowMove(int X, int Y, string name, bool color)
        {
            if (name == "Pawn")
            {
                if (color)
                {
                    // Если пешка на стоковой координате
                    if (Y == 6) {
                        GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;
                        GridosCoordinatos(X, Y - 2).GridElement.Background = Brushes.LightGreen;
                        return;
                    }
                    Classes.Pawn Pawn = Pawns.Find(x => x.X == X && x.Y == Y - 1); // Есть ли кто-то спереди
                    Classes.Pawn PawnLeft = Pawns.Find(x => (x.X == X - 1 && x.Y == Y - 1)); // Есть ли кто-то по диагонали слева
                    Classes.Pawn PawnRight = Pawns.Find(x => (x.X == X + 1 && x.Y == Y - 1)); // Есть ли кто-то по диагонали справа
                    if (Pawn != null)
                    {
                        GridosCoordinatos(Pawn.X, Pawn.Y).GridElement.Background = Brushes.Red;
                    }
                    else
                    {
                        if (Y == 0) return;
                        GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;
                    }
                    if (PawnLeft != null)
                    {
                        GridosCoordinatos(PawnLeft.X, PawnLeft.Y).GridElement.Background = Brushes.LightGreen;
                    }
                    if (PawnRight != null)
                    {
                        GridosCoordinatos(PawnRight.X, PawnRight.Y).GridElement.Background = Brushes.LightGreen;
                    }
                }
                else
                {
                    // Если пешка на стоковой координате
                    if (Y == 1)
                    {
                        GridosCoordinatos(X, Y + 1).GridElement.Background = Brushes.LightGreen;
                        GridosCoordinatos(X, Y + 2).GridElement.Background = Brushes.LightGreen;
                        return;
                    }
                    Classes.Pawn Pawn = Pawns.Find(x => x.X == X && x.Y == Y + 1); // Есть ли кто-то перед нами
                    Classes.Pawn PawnLeft = Pawns.Find(x => (x.X == X - 1 && x.Y == Y + 1)); // Есть ли кто-то по диагонали слева
                    Classes.Pawn PawnRight = Pawns.Find(x => (x.X == X + 1 && x.Y == Y + 1)); // Есть ли кто-то по диагонали справа
                    if (Pawn != null)
                    {
                        GridosCoordinatos(Pawn.X, Pawn.Y).GridElement.Background = Brushes.Red;
                    }
                    else
                    {
                        if (Y == 7) return;
                        GridosCoordinatos(X, Y + 1).GridElement.Background = Brushes.LightGreen;
                    }
                    if (PawnLeft != null)
                    {
                        GridosCoordinatos(PawnLeft.X, PawnLeft.Y).GridElement.Background = Brushes.LightGreen;
                    }
                    if (PawnRight != null)
                    {
                        GridosCoordinatos(PawnRight.X, PawnRight.Y).GridElement.Background = Brushes.LightGreen;
                    }
                }
            }

            if (name == "King")
            {
                if (color)
                {
                    Classes.Pawn KingTop = Pawns.Find(x => x.X == X && x.Y == Y - 1 && x.Black); // Есть ли над нами наши пешки
                    Classes.Pawn KingLeft = Pawns.Find(x => x.X == X - 1 && x.Y == Y && x.Black); // Есть ли слева наши пешки
                    Classes.Pawn KingRight = Pawns.Find(x => x.X == X + 1 && x.Y == Y && x.Black); // Есть ли справа наши пешки
                    Classes.Pawn KingLeftDUp = Pawns.Find(x => x.X == X - 1 && x.Y == Y - 1 && x.Black); // Есть ли слева наши пешки
                    Classes.Pawn KingRightDUp = Pawns.Find(x => x.X == X + 1 && x.Y == Y - 1 && x.Black); // Есть ли справа наши пешки

                    if (Y == 7)
                    {

                        if (KingTop != null)
                        {
                            GridosCoordinatos(KingTop.X, KingTop.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeft != null)
                        {
                            GridosCoordinatos(KingLeft.X, KingLeft.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingRight != null)
                        {
                            GridosCoordinatos(KingRight.X, KingRight.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDUp != null)
                        {
                            GridosCoordinatos(KingLeftDUp.X, KingLeftDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDUp != null)
                        {
                            GridosCoordinatos(KingRightDUp.X, KingRightDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y - 1).GridElement.Background = Brushes.LightGreen;

                        return;
                    }
                    else
                    {
                        Classes.Pawn KingLeftDDown = Pawns.Find(x => x.X == X - 1 && x.Y == Y + 1 && x.Black); // Есть ли слева внизу наши пешки
                        Classes.Pawn KingRightDDown = Pawns.Find(x => x.X == X + 1 && x.Y == Y + 1 && x.Black); // Есть ли справа внизу наши пешки
                        Classes.Pawn KingDown = Pawns.Find(x => x.X == X && x.Y == Y + 1 && x.Black); // Есть ли под нами наши пешки

                        if (KingTop != null)
                        {
                            GridosCoordinatos(KingTop.X, KingTop.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingDown != null)
                        {
                            GridosCoordinatos(KingDown.X, KingDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeft != null)
                        {
                            GridosCoordinatos(KingLeft.X, KingLeft.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingRight != null)
                        {
                            GridosCoordinatos(KingRight.X, KingRight.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDUp != null)
                        {
                            GridosCoordinatos(KingLeftDUp.X, KingLeftDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDUp != null)
                        {
                            GridosCoordinatos(KingRightDUp.X, KingRightDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDDown != null)
                        {
                            GridosCoordinatos(KingLeftDDown.X, KingLeftDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDDown != null)
                        {
                            GridosCoordinatos(KingRightDDown.X, KingRightDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y + 1).GridElement.Background = Brushes.LightGreen;
                    }
                }
                else
                {
                    Classes.Pawn KingDown = Pawns.Find(x => x.X == X && x.Y == Y + 1 && !x.Black); // Есть ли внизу нас наши пешки
                    Classes.Pawn KingLeft = Pawns.Find(x => x.X == X - 1 && x.Y == Y && !x.Black); // Есть ли слева наши пешки
                    Classes.Pawn KingRight = Pawns.Find(x => x.X == X + 1 && x.Y == Y && !x.Black); // Есть ли справа наши пешки
                    Classes.Pawn KingLeftDDown = Pawns.Find(x => x.X == X - 1 && x.Y == Y + 1 && !x.Black); // Есть ли слева внизу наши пешки
                    Classes.Pawn KingRightDDown = Pawns.Find(x => x.X == X + 1 && x.Y == Y + 1 && !x.Black); // Есть ли справа внизу наши пешки

                    if (Y == 0)
                    {
                        if (KingDown != null)
                        {
                            GridosCoordinatos(KingDown.X, KingDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeft != null)
                        {
                            GridosCoordinatos(KingLeft.X, KingLeft.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingRight != null)
                        {
                            GridosCoordinatos(KingRight.X, KingRight.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDDown != null)
                        {
                            GridosCoordinatos(KingLeftDDown.X, KingLeftDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDDown != null)
                        {
                            GridosCoordinatos(KingRightDDown.X, KingRightDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y + 1).GridElement.Background = Brushes.LightGreen;

                        return;
                    }
                    else
                    {
                        Classes.Pawn KingTop = Pawns.Find(x => x.X == X && x.Y == Y - 1 && !x.Black); // Есть ли над нами наши пешки
                        Classes.Pawn KingLeftDUp = Pawns.Find(x => x.X == X - 1 && x.Y == Y - 1 && !x.Black); // Есть ли слева сверху наши пешки
                        Classes.Pawn KingRightDUp = Pawns.Find(x => x.X == X + 1 && x.Y == Y - 1 && !x.Black); // Есть ли справа сверху наши пешки

                        if (KingTop != null)
                        {
                            GridosCoordinatos(KingTop.X, KingTop.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingDown != null)
                        {
                            GridosCoordinatos(KingDown.X, KingDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeft != null)
                        {
                            GridosCoordinatos(KingLeft.X, KingLeft.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingRight != null)
                        {
                            GridosCoordinatos(KingRight.X, KingRight.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDDown != null)
                        {
                            GridosCoordinatos(KingLeftDDown.X, KingLeftDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDDown != null)
                        {
                            GridosCoordinatos(KingRightDDown.X, KingRightDDown.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y + 1).GridElement.Background = Brushes.LightGreen;

                        if (KingLeftDUp != null)
                        {
                            GridosCoordinatos(KingLeftDUp.X, KingLeftDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X - 1, Y - 1).GridElement.Background = Brushes.LightGreen;

                        if (KingRightDUp != null)
                        {
                            GridosCoordinatos(KingRightDUp.X, KingRightDUp.Y).GridElement.Background = Brushes.Red;
                        }
                        else GridosCoordinatos(X + 1, Y - 1).GridElement.Background = Brushes.LightGreen;
                    }
                }
            }
        }

        public void ResetColor ()
        {
            foreach (Classes.Grides Grids in Grids)
            {
                Color color = Color.FromArgb(0xFF, 0x9A, 0x64, 0x00);
                Brush myBrush = new SolidColorBrush(color);

                if ((Grids.tileX + Grids.tileY) % 2 == 0) Grids.GridElement.Background = Brushes.White;
                else Grids.GridElement.Background = myBrush;
            }
        }
    }
}

