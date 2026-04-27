using System.ComponentModel.DataAnnotations;

namespace praktika33.Models
{
    /// <summary> Модель пользователя системы
    public class User
    {
        /// <summary> Идентификатор пользователя
        [Key]
        public int Id { get; set; }
        /// <summary> Логин пользователя
        public string Login {  get; set; }
        /// <summary> Пароль пользователя
        public string Password { get; set; }
        /// <summary> Дата и время последней авторизации пользователя
        public DateTime? LastAuth { get; set; }
    }
}
