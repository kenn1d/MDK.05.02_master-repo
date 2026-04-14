using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika6.Classes
{
    public class Figure
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Figure(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
