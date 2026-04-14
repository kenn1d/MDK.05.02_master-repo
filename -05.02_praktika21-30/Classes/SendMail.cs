using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace praktika21_30_.Classes
{
    //public class SendMail
    //{
    //    readonly static string psw = "nsetyykyfykkbshu";

    //    public static void SendMessage(string Message, string To)
    //    {
    //        // Содаём smtp клиент, в качестве хоста указываем яндекс
    //        var smtpClient = new SmtpClient("smtp.yandex.ru")
    //        {
    //            Port = 587,
    //            Credentials = new NetworkCredential("mak.gam@yandex.ru", "password"),
    //            EnableSsl = true,
    //        };
    //        // Вызываем метод send, который отправляет письмо на указанный адрес
    //        smtpClient.Send("mak.gam@yandex.ru", To, "Проект Praktika21(30)", Message);
    //    }
    //}

    public class SendMail
    {
        // ⚠️ Укажите реальный пароль (или пароль приложения)
        readonly static string psw = "nsetyykyfykkbshu";

        public static void SendMessage(string Message, string To)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.yandex.ru")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("mak.gam@yandex.ru", psw),
                })
                {
                    // Явно указываем способ аутентификации
                    smtpClient.TargetName = "STARTTLS/smtp.yandex.ru";

                    smtpClient.Send(
                        "mak.gam@yandex.ru",
                        To,
                        "Проект Praktika21(30)",
                        Message
                    );
                }
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"Ошибка SMTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось отправить письмо: {ex.Message}");
            }
        }
    }
}
