using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace praktika27.ViewModell
{
    public class VMItems : INotifyPropertyChanged
    {
        /// <summary> Коллекция предметов
        public ObservableCollection<Context.ItemsContext> _items;
        public ObservableCollection<Context.ItemsContext> Items { get => _items; set { 
                _items = value;
                OnProperityChanged();
            } }

        /// <summary> Команда для добавления нового элемента
        public Classes.RelayCommand NewItem
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    // Создаём новую модель, сохраняя её в БД
                    Context.ItemsContext newModell = new Context.ItemsContext(true);
                    // Добавляем модель в коллекцию
                    Items.Add(newModell);
                    // Переходим на страницу добавления, указывая в качестве контекста
                    // Модель, которая была только что создана
                    MainWindow.init.frame.Navigate(new View.Add(newModell));
                });
            }
        }

        /// <summary> Конструктор модели представления
        public VMItems() =>
            // Загружаем предметы из БД
            Items = Context.ItemsContext.AllItems();

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
