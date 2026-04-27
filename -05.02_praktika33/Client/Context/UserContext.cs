using Client.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Client.Context
{
    public class UserContext
    {
        /// <summary>Базовый URL API для пользовательских операций</summary>
        static string url = "https://localhost:7172/user/";

        /// <summary>
        /// Асинхронный метод аутентификации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>JWT токен при успешном входе, null при ошибке</returns>
        public static async Task<string> Login(string login, string password)
        {
            // Создаем HTTP клиент для отправки запроса 
            using (HttpClient Client = new HttpClient())
            {
                // Создаем HTTP запрос с методом POST на URL логина 
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "login"))
                {
                    // Формируем данные для отправки в формате application/x-www-form-urlencoded 
                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["login"] = login,     // Логин из параметра
                        ["password"] = password // Пароль из параметра
                    };
                    // Создаем контент запроса из данных формы 
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    // Устанавливаем контент в запрос 
                    Request.Content = Content;
                    // Отправляем запрос и ждем ответ 
                    var Response = await Client.SendAsync(Request);
                    // Проверяем статус ответа 
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        // Читаем JSON ответ от сервера 
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        // Десериализуем JSON в объект Auth 
                        // Ожидаемая структура: { "token": "jwt-token-value" }
                        Auth DataAuth = JsonConvert.DeserializeObject<Auth>(sResponse);
                        // Возвращаем токен 
                        return DataAuth.Token;
                    }
                }
                // Возвращаем null в случае ошибки или неверных учетных данных 
                return null;
            }
        }

        /// <summary>
        /// Асинхронный метод регистрации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>True при успехе, False при неудаче</returns>
        public static async Task<bool> Regin(string login, string password)
        {
            // Создаем HTTP клиент для отправки запроса 
            using (HttpClient Client = new HttpClient())
            {
                // Создаем HTTP запрос с методом POST на URL логина 
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "regin"))
                {
                    // Формируем данные для отправки в формате application/x-www-form-urlencoded 
                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["login"] = login,     // Логин из параметра
                        ["password"] = password // Пароль из параметра
                    };
                    // Создаем контент запроса из данных формы 
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    // Устанавливаем контент в запрос 
                    Request.Content = Content;
                    // Отправляем запрос и ждем ответ 
                    var Response = await Client.SendAsync(Request);
                    // Проверяем статус ответа 
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        // Возвращаем True
                        return true;
                    }
                }
                // Возвращаем False в случае ошибки 
                return false;
            }
        }
    }
}