using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Client.Context
{
    public class StorageContext
    {
        /// <summary>
        /// Базовый URL API для операций с хранилищами
        /// </summary>
        static string url = "https://localhost:7172/storage/";

        /// <summary>
        /// Получение всех записей хранилища для текущего пользователя
        /// GET /storage/get
        /// </summary>
        /// <returns>Список записей или null при ошибке</returns>
        public static async Task<List<Models.Storage>?> Get()
        {
            // Создаем экземпляр HTTP клиента для выполнения запроса
            using (HttpClient Client = new HttpClient())
            {

                // Формируем GET-запрос, объединяя базовый URL и эндпоинт "get"
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Get, url + "get"))
                {

                    // Добавляем в заголовки запроса токен авторизации из MainWindow
                    Request.Headers.Add("token", MainWindow.Token);

                    // Отправляем запрос на сервер асинхронно и ждем ответа
                    var Response = await Client.SendAsync(Request);

                    // Проверяем, вернул ли сервер успешный статус-код 200 OK
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        // Считываем содержимое ответа в виде строки
                        string sResponse = await Response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-строку в список объектов типа Storage
                        List<Models.Storage> Storages = JsonConvert.DeserializeObject<List<Models.Storage>>(sResponse);

                        // Возвращаем полученный список
                        return Storages;
                    }
                }
            }

            // Если произошла ошибка или статус-код не 200, возвращаем null
            return null;
        }

        /// <summary>
        /// Добавление новой записи в хранилище
        /// POST /storage/add
        /// </summary>
        /// <param name="storage">Объект для добавления (без ID)</param>
        /// <returns>Созданный объект с присвоенным ID или null при ошибке</returns>
        public static async Task<Models.Storage> Add(Models.Storage storage)
        {
            // Инициализируем HTTP клиент для отправки запроса
            using (HttpClient Client = new HttpClient())
            {
                // Создаем POST-запрос по адресу url + "add"
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "add"))
                {
                    // Добавляем токен авторизации из MainWindow в заголовки
                    Request.Headers.Add("token", MainWindow.Token);

                    // Сериализуем объект Storage в формат JSON
                    string JsonStorage = JsonConvert.SerializeObject(storage);

                    // Создаем контент для тела запроса (указываем JSON, кодировку UTF8 и тип контента)
                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;

                    // Отправляем запрос на сервер асинхронно
                    var Response = await Client.SendAsync(Request);

                    // Проверяем, если сервер вернул статус 200 OK
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        // Читаем тело ответа сервера как строку
                        string sResponse = await Response.Content.ReadAsStringAsync();

                        // Десериализуем ответ (сервер возвращает созданный объект с присвоенным ID)
                        Models.Storage Storage = JsonConvert.DeserializeObject<Models.Storage>(sResponse);

                        // Возвращаем полученный объект
                        return Storage;
                    }
                }
            }

            // Возвращаем null, если запрос не удался или статус не OK
            return null;
        }

        /// <summary>
        /// Обновление существующей записи
        /// PUT /storage/update
        /// </summary>
        /// <param name="storage">Объект с обновленными данными (должен содержать ID)</param>
        /// <returns>Обновленный объект или null при ошибке</returns>
        public static async Task<Models.Storage> Update(Models.Storage storage)
        {
            // Инициализируем HTTP клиент для выполнения запроса
            using (HttpClient Client = new HttpClient())
            {

                // Создаем PUT-запрос к эндпоинту "update"
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Put, url + "update"))
                {

                    // Добавляем токен авторизации в заголовок запроса
                    Request.Headers.Add("token", MainWindow.Token);

                    // Сериализуем обновляемый объект в формат JSON
                    string JsonStorage = JsonConvert.SerializeObject(storage);

                    // Подготавливаем контент: упаковываем JSON, ставим кодировку UTF8 и тип application/json
                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;

                    // Асинхронно отправляем запрос на сервер
                    var Response = await Client.SendAsync(Request);

                    // Проверяем, если сервер подтвердил успех (статус 200 OK)
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {

                        // Считываем тело ответа от сервера
                        string sResponse = await Response.Content.ReadAsStringAsync();

                        // Десериализуем ответ обратно в объект Storage
                        Models.Storage Storage = JsonConvert.DeserializeObject<Models.Storage>(sResponse);

                        // Возвращаем результат
                        return Storage;
                    }
                }
            }

            // В случае неудачи возвращаем null
            return null;
        }

        /// <summary>
        /// Удаление записи по ID
        /// DELETE /storage/delete
        /// </summary>
        /// <param name="id">ID удаляемой записи</param>
        public static async Task Delete(int id)
        {
            // Инициализируем HTTP клиент для выполнения операции удаления
            using (HttpClient Client = new HttpClient())
            {

                // Создаем DELETE-запрос к эндпоинту "delete"
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Delete, url + "delete"))
                {

                    // Добавляем токен авторизации в заголовок запроса
                    Request.Headers.Add("token", MainWindow.Token);

                    // Для DELETE запроса данные отправляются как form-data
                    // Создаем словарь с ID записи для удаления
                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["id"] = id.ToString() // ID записи для удаления
                    };

                    // Создаем контент как форму (x-www-form-urlencoded)
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    Request.Content = Content;

                    // Отправляем запрос на сервер асинхронно
                    var Response = await Client.SendAsync(Request);

                    // Если успешно (можно проверить статус)
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        // Читаем ответ (обычно пустой или подтверждение)
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        // Можно залогировать или проигнорировать
                    }
                }
            }
        }
    }
}
