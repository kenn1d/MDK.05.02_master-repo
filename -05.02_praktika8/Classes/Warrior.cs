using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace praktika8.Classes
{
    public class Warrior
    {
        public int Health { get; set; }

        public Warrior(int Health)
        {
            this.Health = Health;
        }

        public virtual void SetDamadge(int damage)
        {
            this.Health -= damage;
        }
    }
}
