using Microsoft.AspNetCore.Mvc;
using praktika33.Classes;
using praktika33.Models;

namespace praktika33.Controllers
{
    /// <summary> Контроллер для управления хранилищем паролей пользователя
    [Route("/storage")]
    public class StorageController : Controller
    {
        /// <summary>
        /// Менеджер для работы с БД
        /// </summary>
        private DatabaseManager databaseManager;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public StorageController() => this.databaseManager = new DatabaseManager();

        /// <summary>
        /// Получение всеъ записей хранилища для авторизованного пользователя
        /// </summary>
        /// <param name="token">JWT токен из заголовка запроса</param>
        /// <returns>Список записей хранилища в формате DTO (без информации о пользователе)</returns>
        [Route("get")]
        [HttpGet]
        public ActionResult Get([FromHeader] string token)
        {
            try
            {
                // Извлекаем id пользователя из токена
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                // Если токен не действителен или ID не получен - возвращаем 401 Unauthrized
                if (IdUser == null) return StatusCode(401);
                // Получаем все записи текущего пользователя и преобразуем их в DTO
                // DTO используется чтобы скрыть информацию о пользователе из ответа
                List<StorageDto> Storages = databaseManager.Storages
                    .Where(x => x.User.Id == IdUser) // Фильтруем по Id пользователя
                    .Select(s => new StorageDto
                    {
                        // Проецируем в StorageDto
                        Id = s.Id,
                        Name = s.Name,
                        Url = s.Url,
                        Login = s.Login,
                        Password = s.Password
                    }).ToList();
                // Возвращаем 200 ОК со списком записей
                return Ok(Storages);
            }
            catch (Exception ex)
            {
                // В случае любой ошибки возвращаем 501
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Доабвление новой записи в хранилище
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="storage">Данные новой записи (JSON в теле запроса)</param>
        /// <returns>Добавленная запись</returns>
        [Route("add")]
        [HttpPost]
        public ActionResult Add([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                // Валидация токена
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                if (IdUser == null) return StatusCode(401);
                // Находим пользователя в БД и привязываем к новой записи
                storage.User = databaseManager.Users
                    .Where(x => x.Id == IdUser).First(); // Выбросится исключение, если пользователь не найден
                // Добавляем запись в БД
                databaseManager.Add(storage);
                databaseManager.SaveChanges();

                // Обнуляем ссылку на пользователя, чтобы избежать циклической ссылки в JSON
                storage.User = null;
                // Возвращаем созданную запись
                return StatusCode(200, storage);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Обеновление существующей записи
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="storage">Обновленные данные записи</param>
        /// <returns>Обновленная запись</returns>
        [Route("update")]
        [HttpPut]
        public ActionResult Update([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                // Валидация токена
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                // Ищем существующую запись в БД по ID
                Storage? uStorage = databaseManager.Storages
                    .Where(x => x.Id == storage.Id)
                    .FirstOrDefault();

                if (IdUser == null) return StatusCode(401);
                if (uStorage == null) return StatusCode(404); // Запись не найдена

                uStorage.Name = storage.Name;
                uStorage.Url = storage.Url;
                uStorage.Login = storage.Login;
                uStorage.Password = storage.Password;

                // Обнуляем ссылку на пользователя, чтобы избежать циклической ссылки в JSON
                storage.User = null;
                // Возвращаем созданную запись
                return StatusCode(200, storage);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Обеновление существующей записи
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="id">id удаляемой записи (из формы)</param>
        /// <returns>Статус выполнения операции</returns>
        [Route("delete")]
        [HttpDelete]
        public ActionResult Update([FromHeader] string token, [FromForm] int id)
        {
            try
            {
                // Валидация токена
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                // Ищем существующую запись в БД по ID
                Storage? Storage = databaseManager.Storages
                    .Where(x => x.Id == id && x.User.Id == IdUser)
                    .FirstOrDefault();

                if (IdUser == null) return StatusCode(401);
                if (Storage == null) return StatusCode(404);

                databaseManager.Storages.Remove(Storage);
                databaseManager.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }
    }
}
