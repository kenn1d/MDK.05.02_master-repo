using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Afisha
    {
        public int Id { get; set; }
        public int IdKinoteatr { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int Price { get; set; }

        public Afisha(int Id, int IdKinoteatr, string Name, DateTime Time, int Price)
        {
            this.Id = Id;
            this.IdKinoteatr = IdKinoteatr;
            this.Name = Name;
            this.Time = Time;
            this.Price = Price;
        }
    }
}
