using Common;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public int StepCadr = 0;

        public Game()
        {
            InitializeComponent();
        }

        public void CreateUI(List<ViewModelGames> PlayersList)
        {
            Dispatcher.Invoke(() =>
            {
                if (StepCadr == 0) StepCadr = 1;
                else StepCadr = 0;

                canvas.Children.Clear();
                for (int i = 0; i < PlayersList.Count; i++)
                {
                    for (int iPoint = PlayersList[i].SnakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                    {
                        // Получаем точку
                        Snakes.Point SnakePoint = PlayersList[i].SnakesPlayers.Points[iPoint];

                        // Смещение точек змеи
                        if (iPoint != 0)
                        {
                            // получаем следующую точку змеи
                            Snakes.Point NextSnakePoint = PlayersList[i].SnakesPlayers.Points[iPoint - 1];
                            // если точка по горизонтали
                            if (SnakePoint.X > NextSnakePoint.X || SnakePoint.X < NextSnakePoint.X)
                            {
                                // если точка чётная
                                if (iPoint % 2 == 0)
                                {
                                    // если кадр чётный 
                                    if (StepCadr % 2 == 0) SnakePoint.Y -= 1;
                                    else SnakePoint.Y += 1;
                                }
                                else
                                {
                                    if (StepCadr % 2 == 0) SnakePoint.Y += 1;
                                    else SnakePoint.Y -= 1;
                                }
                            }
                            // если точка по вертикали
                            else if (SnakePoint.Y > NextSnakePoint.Y || SnakePoint.Y < NextSnakePoint.Y)
                            {
                                if (iPoint % 2 == 0)
                                {
                                    if (StepCadr % 2 == 0) SnakePoint.X -= 1;
                                    else SnakePoint.X += 1;
                                }
                                else
                                {
                                    if (StepCadr % 2 == 0) SnakePoint.X += 1;
                                    else SnakePoint.X -= 1;
                                }
                            }
                        }
                        // цвет точки
                        Brush Color;
                        // если первая точка
                        if (iPoint == 0)
                            Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 127, 14));
                        else
                            Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 198, 19));

                        Ellipse ellipse = new Ellipse()
                        {
                            Width = 20,
                            Height = 20,
                            Margin = new System.Windows.Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                            Fill = Color,
                            Stroke = Brushes.Black
                        };
                        canvas.Children.Add(ellipse);
                    }
                    // отрисовка яблока
                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Image/Apple.png"));
                    Ellipse points = new Ellipse()
                    {
                        Width = 40,
                        Height = 40,
                        // -20 центрирование яблока
                        Margin = new System.Windows.Thickness(
                            PlayersList[i].Points.X - 20,
                            PlayersList[i].Points.Y - 20, 0, 0),
                        Fill = myBrush
                    };
                    canvas.Children.Add(points);
                }
            });
        }
    }
}
