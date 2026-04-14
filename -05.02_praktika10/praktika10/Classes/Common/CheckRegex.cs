using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace praktika10.Classes.Common
{
    public class CheckRegex
    {
        ///<summary>
        /// Метод поиска совпадений
        /// </summary>
        /// <param name="Pattern">Регулярное выражение</param>
        /// <param name="Input">Строка ввода</param>
        /// <returns></returns>
        public static bool Match(string Pattern, string Input)
        {
            // Создаём регулярное выражение и ищим совпадения по паттерну
            Match m = Regex.Match(Input, Pattern);
            // Совмещаем результат
            return m.Success;
        }
    }
}
