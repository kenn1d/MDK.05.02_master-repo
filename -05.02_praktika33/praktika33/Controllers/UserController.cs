using Microsoft.AspNetCore.Mvc;
using praktika33.Classes;
using praktika33.Models;
using System.Runtime.Intrinsics.X86;

namespace praktika33.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        /// <summary>
        /// Приватное поле для хранения экземпляра DatabaseManager
        /// Используется для работы с БД
        /// </summary>
        private DatabaseManager databaseManager;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public UserController()
        {
            // Сохраняем полученный экземплаяр DatabaseManager в приватное поле
            this.databaseManager = this.databaseManager = new DatabaseManager();
        }

        /// <summary>
        /// Метод для аутентификации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>JWT токен или код ошибки</returns>
        [Route("login")] // Доп. маршрут: /user/login
        [HttpPost] // Только POST запросы
        public ActionResult Login([FromForm] string login, [FromForm] string password)
        {
            try
            {
                // Ищем пользователя в БД по логину
                User? AuthUser = databaseManager.Users
                    .Where(x => x.Login == login)
                    .FirstOrDefault();

                // Проверяем существует ли пользователь по логину, совпадает ли введённый пароль с хэшированным паролем
                if (AuthUser != null && Hasher.CheckHashPsw(password, AuthUser.Password)) {
                    // Генерим JWT токен для аутентификационного пользователя
                    string Token = JwtToken.Generate(AuthUser);
                    // Обновляем дату последней авторизации
                    AuthUser.LastAuth = DateTime.Now;
                    // Сохраняем изменения в БД
                    databaseManager.SaveChanges();
                    // Возвращаем успешный ответ с токеном
                    return Ok(new { token = Token });
                }
                else {
                    // Возвращаем 401 (unauthorized)
                    return StatusCode(401);
                }
            }
            catch (Exception ex)
            {
                // В случае любой ошибки дропаем 501 (not implemented)
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Метод для регистрации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Успех или код ошибки</returns>
        [Route("regin")] // Доп. маршрут: /user/regin
        [HttpPost] // Только POST запросы
        public ActionResult Regin([FromForm] string login, [FromForm] string password)
        {
            try
            {
                // Ищем пользователя в БД по логину
                User? AuthUser = databaseManager.Users
                    .Where(x => x.Login == login)
                    .FirstOrDefault();
                // Проверяем существует ли пользователь по логину
                if (AuthUser != null)
                {
                    // Возвращаем 403 (Forbidden) - запрет дубликата
                    return StatusCode(403);
                }
                else
                {
                    User newUser = new User() {
                        Login = login,
                        Password = Hasher.HashPsw(password)
                    };
                    databaseManager.Add(newUser);
                    // Сохраняем изменения в БД
                    databaseManager.SaveChanges();
                    // Возвращаем успешный ответ
                    return StatusCode(200);
                    
                }
            }
            catch (Exception ex)
            {
                // В случае любой ошибки дропаем 501 (not implemented)
                return StatusCode(501, ex.Message);
            }
        }
    }
}
