using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace praktika33.Models
{
    /// <summary> Модель хранилища учетных данных
    public class Storage
    {
        /// <summary> Первичный ключ
        [Key]
        public int Id { get; set; }
        /// <summary> Название записи/сервиса (Google, Github и т.д.)
        public string Name { get; set; }
        /// <summary> URL-адрес сервиса
        public string? Url { get; set; }
        /// <summary> Логин для сервиса
        public string Login {  get; set; }
        /// <summary> Пароль для сервиса
        public string Password { get; set; }
        /// <summary> Пользователь, которому принадлежит ключ
        [ForeignKey("userId")]
        public User User { get; set; }
    }
}
