using praktika27.Context;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace praktika27.ViewModell
{
    public class VMCategorys : INotifyPropertyChanged
    {
        /// <summary> Коллекция предметов
        public ObservableCollection<Context.CategorysContext> Categorys { get; set; }

        /// <summary> Конструктор модели представления
        public VMCategorys() =>
            // Загружаем предметы из БД
            Categorys = CategorysContext.AllCategorys();

        public Classes.RelayCommand NewCategory
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    // Создаём новую модель, сохраняя её в БД
                    Context.CategorysContext newModell = new Context.CategorysContext(true);
                    // Добавляем модель в коллекцию
                    Categorys.Add(newModell);
                    // Переходим на страницу добавления, указывая в качестве контекста
                    // Модель, которая была только что создана
                    MainWindow.init.frame.Navigate(new View.AddCategory(newModell));
                });
            }
        }

        /// <summary> Событие изменения свойства
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Метод, сообщающий системе об изменении свойства
        public void OnProperityChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
