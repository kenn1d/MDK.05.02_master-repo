using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using praktika28.Classes;
using praktika28.Context;
using praktika28.Models;

namespace praktika28.ViewModels
{
    public class VMTasks : Notification
    {
        public TasksContext taskContext = new TasksContext();

        public ObservableCollectionListSource<Tasks> Tasks { get; set; }
        public ObservableCollectionListSource<Sorted> Sorted { get; set; }

        /// <summary>
        /// Конструктор для модели представения
        /// </summary>
        public VMTasks() // Получаем данные из контекста данных отсортировав по возрастанию
        {
            Tasks = new ObservableCollectionListSource<Tasks>(taskContext.Tasks.OrderBy(x => x.Done));
        }
            

        /// <summary>
        /// Метод добавления новой задачи
        /// </summary>
        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obj => // выолняем команду
                {
                    Tasks NewTask = new Tasks() // создаём новую задачу
                    {
                        DateExecute = DateTime.Now // устанавливаем текущую дату
                    };
                    Tasks.Add(NewTask); // добавляем задачу в коллекцию
                    taskContext.Tasks.Add(NewTask); // добавляем задачу в контекст данных
                    taskContext.SaveChanges(); // сохраняем изменения в БД
                });
            }
        }

        public string nameSearch;
        public string NameSearch {
            get => nameSearch; 
            set
            {
                nameSearch = value;
                ObservableCollectionListSource<Tasks> find = new ObservableCollectionListSource<Tasks>(taskContext.Tasks.Where(x => x.Name == nameSearch));
                if (find.Any())
                {
                    Tasks = find;
                }
                else Tasks = new ObservableCollectionListSource<Tasks>(taskContext.Tasks.OrderBy(x => x.Done));
                OnPropertyChanged("Tasks");
            }
        }

        public Tasks.EPriority prioritySort;
        public Tasks.EPriority PrioritySort
        {
            get => prioritySort;
            set
            {
                prioritySort = value;
                ObservableCollectionListSource<Tasks> find = new ObservableCollectionListSource<Tasks>(taskContext.Tasks.OrderByDescending(x => x.Priority == prioritySort));
                if (find.Any())
                {
                    Tasks = find;
                }
                else Tasks = new ObservableCollectionListSource<Tasks>(taskContext.Tasks.OrderBy(x => x.Done));
                OnPropertyChanged("Tasks");
            }
        }

        public RealyCommand GetTable
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    var find = taskContext.Tasks.AsNoTracking().Where(x => x.Priority == prioritySort).ToList();
                    if (find.Any())
                    {
                        taskContext.Sorted.RemoveRange(taskContext.Sorted);
                        foreach (var Task in find)
                        {
                            var sortedTask = new Sorted(Task);
                            taskContext.Sorted.Add(sortedTask);
                        }
                        taskContext.SaveChanges();
                    }
                    else MessageBox.Show("Записей не найдено", "Внимание");
                });
            }
        }
    }
}
