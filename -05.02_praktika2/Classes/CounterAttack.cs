using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika2.Classes
{
    public class CounterAttack
    {
        /// <summary>
        /// Получение вероятности контратаки
        /// </summary>
        /// <returns>Вероятность</returns>
        public static double P()
        {
            Random random = new Random();
            double P = random.NextDouble();

            return P;
        }
    }
}
