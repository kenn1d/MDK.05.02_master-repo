using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace praktika27.Modell
{
    public class Items : INotifyPropertyChanged
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

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private Categorys category;
        public Categorys Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
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
