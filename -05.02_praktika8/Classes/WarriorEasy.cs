using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika8.Classes
{
    public class WarriorEasy : Warrior
    {
        public WarriorEasy(int Health) : base(Health) { }

        public override void SetDamadge(int damage)
        {
            Health -= damage;
        }
    }
}
