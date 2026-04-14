using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika14.Classes
{
    public class Category
    {
        public string name { get; set; }
        public List<Classes.Item> items = new List<Classes.Item>();

        public Category(string name, List<Classes.Item> items) {
            this.name = name;
            this.items = items;
        }
    }
}
