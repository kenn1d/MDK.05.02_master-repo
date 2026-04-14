using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess_Kibanov.Classes
{
    public class Grides
    {
        public int tileX, tileY;
        public Grid GridElement { get; set; }

        public object Index;
        

        public Grides(int X, int Y, Grid grid)
        {
            tileX = X;
            tileY = Y;
            GridElement = grid;
        }
    }
}
