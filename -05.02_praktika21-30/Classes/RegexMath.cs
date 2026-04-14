using System.Text.RegularExpressions;

namespace praktika21_30_.Classes
{
    public class RegexMath
    {
        public static bool Math(bool bCorrect, string regularExpression, string expression) { 
            
            Regex regex = new Regex(regularExpression);
            bCorrect = regex.IsMatch(expression);
            
            return bCorrect;
        }
    }
}
