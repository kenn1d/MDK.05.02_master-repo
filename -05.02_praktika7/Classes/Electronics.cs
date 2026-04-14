using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika7.Classes
{
    public class Electronics : Shop
    {
        public int ValueAKB { get; set; }
        public int Speed { get; set; }
        public Electronics(string Name, int Price, int ValueAKB, int Speed) : base(Name, Price) {
            this.ValueAKB = ValueAKB;
            this.Speed = Speed;
        }
    }
}
