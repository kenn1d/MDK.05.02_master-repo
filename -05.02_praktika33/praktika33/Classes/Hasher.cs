using System.Security.Cryptography;

namespace praktika33.Classes
{
    public class Hasher
    {
        /// <summary>
        /// Создаёт хэш пароля с "солью"
        /// </summary>
        /// <param name="password">Пароль в открытом виде</param>
        /// <returns>Захэшированный пароль с "солью"</returns>
        public static string HashPsw(string password)
        {
            // Создаём массив "соли"
            byte[] salt = new byte[16];
            // Заполняем массив рандомными значениями
            RandomNumberGenerator.Fill(salt);

            // Создаём хэш на основе пароля и "соли"
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            // Объединяем "соль" и хэш в один массив для удобства хранения
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Возвращаем строку как base64
            return Convert.ToBase64String(hashBytes);
        }

        public static bool CheckHashPsw(string password, string savedHash)
        {
            // Извлекаем байты из хэша
            byte[] hashBytes = Convert.FromBase64String(savedHash);

            // Извлекаем соль (первые 16 байт)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Создаём подобный хэш на основе пароля и старой "соли"
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            // Сравниваем результат байт за байтом
            for (int i = 0; i < 20; i++) {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}
