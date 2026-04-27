using System.Collections.Generic;

namespace Common
{
    public class Snakes
    {
        public class Point
        {
            public int X {  get; set; }
            public int Y { get; set; }
            public Point (int x, int y)
            {
                this.X = x; this.Y = y;
            }
            public Point() { }
        }

        ///<summary> Направление движения змеи
        public enum Direction
        {
            Left, Right, Up, Down, Start
        }

        ///<summary> Точки из которых состоит змея
        public List<Point> Points = new List<Point>();
        ///<summary> Направление движения в котором двигается змея
        public Direction direction = Direction.Start;
        ///<summary> Переменная сигнализирующая о конце игры
        public bool GameOver = false;
    }
}
