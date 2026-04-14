using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace praktika27.Modell
{
    public class Categorys : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            // аксессор для чтения
            get { return id; }
            // аксессор для записи
            set
            {
                id = value; // записываем значение
                OnPropertyChanged("Id"); // сообщаем о том, что свойство изменилось
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary> Событие изменения свойств
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Метод, сообщающий систее об изменении свойства
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
