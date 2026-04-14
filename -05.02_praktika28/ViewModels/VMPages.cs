using praktika28.Classes;

namespace praktika28.ViewModels
{
    public class VMPages : Notification
    {
        /// <summary>
        /// Модель представления задач
        /// </summary>
        public VMTasks vm_tasks = new VMTasks();

        /// <summary>
        /// Конутруктор модели представления для навигации
        /// </summary>
        public VMPages()
        {
            MainWindow.init.frame.Navigate(new Views.Main(vm_tasks));
        }

        /// <summary>
        /// Команда закрытия приложения
        /// </summary>
        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj => // выполнение команды
                {
                    MainWindow.init.Close(); //закрывает главное окно
                });
            }
        }
    }
}
