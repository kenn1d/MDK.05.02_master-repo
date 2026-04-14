using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika8.Classes
{
    public class RepoWarriors
    {
        public static List<object> AllWarrior() {
            List<object> AllWarriors = new List<object>();

            AllWarriors.Add(new Classes.WarriorEasy(500));
            AllWarriors.Add(new Classes.WarriorHard(1000));

            return AllWarriors;
        }
    }
}
