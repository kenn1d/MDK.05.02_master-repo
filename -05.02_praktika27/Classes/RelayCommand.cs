using System.Windows.Input;

namespace praktika27.Classes
{
    public class RelayCommand : ICommand
    {
        /// <summary> Метод, который будет выполняться
        private Action<object> execute;
        /// <summary> Возможность выполнения метода
        private Func<object, bool> canExecute;

        /// <summary> Конструктор для регистрации выполняемого метода
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary> Событие, которое добавляет или удаляет метод на выполнение
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value; }
        }

        /// <summary> Проверяем, может ли выполниться метод
        public bool CanExecute(object parameter) { 
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary> Выполняем метод
        public void Execute(object parameter) =>
            this.execute(parameter);
    }
}
