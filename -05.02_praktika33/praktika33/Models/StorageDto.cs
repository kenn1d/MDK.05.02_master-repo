namespace praktika33.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) для модели Storage
    /// Используется для передачи данных между слоями приложения
    /// Содержит только те поля, которые нужно отправлять клиенту
    /// </summary>
    public class StorageDto
    {
        /// <summary> Уникальный идентификатор
        public int Id { get; set; }
        /// <summary> Название сервсиа
        public string Name { get; set; }
        /// <summary> URL сервиса
        public string Url { get; set; }
        /// <summary> Логин для сервиса
        public string Login {  get; set; }
        /// <summary> Пароль для сервиса
        public string Password { get; set; }

        // ПОле User намеренно отсутствует - это основная причина создания DTO
        // Мы не хотим отправлять клиенту информацию о пользователе
    }
}
