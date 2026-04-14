using praktika28.Classes;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace praktika28.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }

        protected string name;
        public string Name
        {
            get {
                return name; }
            set
            {
                // проверяем входящее значение, на регулярное выражение
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success) // если нет совпадения
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов.",
                        "Некорректный ввод значеия");
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public enum EPriority
        {
            Высокий, //0
            Средний, //1
            Низкий //2
        }

        protected EPriority priority;
        public EPriority Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged("Priority");
            }
        }

        protected DateTime dateExecute;
        public DateTime DateExecute
        {
            get { return dateExecute; }
            set
            {
                dateExecute = value;
                OnPropertyChanged("DateExecute");
                if (value.Date < DateTime.Now.Date);
            }
        }

        protected string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                Match match = Regex.Match(value, "^.{1,1000}$");
                if (!match.Success)
                    MessageBox.Show("Комментарий не должен быть пустым, и не более 1000 символов.",
                        "Некорректный ввод значеия");
                else
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        protected bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                OnPropertyChanged("Done");
                OnPropertyChanged("IsDoneText");
            }
        }

        [Schema.NotMapped] // исключаем поле из добавления в таблицу БД
        private bool isEnable;

        [Schema.NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [Schema.NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить"; // если изменение включено, возвращаем одно значение
                else return "Изменить"; // иначе
            }
        }

        [Schema.NotMapped]
        public string IsDoneText
        {
            get
            {
                if (IsEnable) return "Не выполнено"; // если изменение включено, возвращаем одно значение
                else return "Выполнено"; // иначе
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj => // выполняем команду
                {
                    IsEnable = !IsEnable; // изменяем состояние изменения представления

                    if (!IsEnable) // если состояние не активно
                        // вызываем сохранение данных в контексте taskContext
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_tasks.taskContext.SaveChanges();
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    // уточняем о то что пользователь хочет удалить объект
                    if (MessageBox.Show("Вы уверены, что хотите удалить задачу?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // удялем модель из коллекции
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_tasks.Tasks.Remove(this);
                        // удялем модель из контекста данных
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_tasks.taskContext.Remove(this);
                        // вызываем сохранение данных в контексте TaskContext
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_tasks.taskContext.SaveChanges();
                    }
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnDone
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Done = !Done;
                });
            }
        }
    }
}
