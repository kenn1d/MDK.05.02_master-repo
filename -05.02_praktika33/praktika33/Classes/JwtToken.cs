using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using praktika33.Models;

namespace praktika33.Classes
{
    /// <summary>
    /// Отвечает за генерацию и валидацию токенов
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// Секретный ключ для подписи токенов
        /// static означает, что ключ общий для всех экземпляров класса
        /// </summary>
        static byte[] Key = Encoding.UTF8.GetBytes("SuperSecretKey_32_Characters_Long_12345");

        /// <summary>
        /// Генерирует JWT токен для пользователя
        /// </summary>
        /// <param name="user">Пользователь, для которого создаётся токен</param>
        /// <returns>Строка с JWT токеном</returns>
        public static string Generate(User user)
        {
            // Создаём обработчик JWT токенов
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            // Описываем содержимое токена
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor { 
                // Добавляем claims (утверждения) в токен
                // Claim - это пары ключ-значение с информацией о пользователе
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.Id.ToString())    // Сохраняем Id пользователя
                }),
                // Время истечения токена (7 дней)
                Expires = DateTime.UtcNow.AddDays(7),
                // Подписываем токе нашим секретным ключрм
                // Это гарантирует, что токен не был подделан
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Key),             // Используем симметричное шифрование
                    SecurityAlgorithms.HmacSha256Signature     // Алгоритм подписи HMAC-SHA256
                )
            };
            // Создаём токен на основе описания
            SecurityToken Token = TokenHandler.CreateToken(tokenDescriptor);
            // Возвращаем токен в виде строки
            return TokenHandler.WriteToken(Token);
        }

        public static int? GetUserIdFromToken(string token)
        {
            try {
                // Содаём обработчик JWT токенов
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                // Валидируем токен и извлекаем данные
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    // Проверяем, что токен подписан нашим ключом
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    // Отключаем проверку издателя и аудитории
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Убираем временную погрешность при проверке срока действия
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                // Преобразуем валидированный токен в JwtSecurityToken
                JwtSecurityToken JwtToken = (JwtSecurityToken)validatedToken;
                // Извлекам значение claim с именем "UserId"
                string UserId = JwtToken.Claims.First(x => x.Type == "UserId").Value;
                // Преобразуем строку в число и возвращаем
                return int.Parse(UserId);
            }
            catch {
                return null;
            }
        }
    }
}
