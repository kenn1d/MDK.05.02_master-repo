
using praktika27.Classes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace praktika27.Context
{
    public class ItemsContext : Modell.Items
    {
        public ItemsContext(bool save = false)
        {
            // если ключ true, сохраняем модель в БД
            if (save) Save(true);
            Category = new Modell.Categorys();
        }

        /// <summary> Метод загрузки данных из БД
        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<CategorysContext> allCategorys = CategorysContext.AllCategorys();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("SELECT * FROM [dbo].[Items]", out connection);
            while (dataItems.Read())
            {
                allItems.Add(new ItemsContext() {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Price = dataItems.GetDouble(2),
                    Description = dataItems.GetString(3),
                    Category = dataItems.IsDBNull(4) ?
                        null : allCategorys.Where(x => x.Id == dataItems.GetInt32(4)).First() 
                });
            }
            Connection.CloseConnection(connection);
            return allItems;
        }

        /// <summary> Метод сохранения модели
        public void Save(bool New = false)
        {
            SqlConnection connection;
            // Если модель новая
            if (New)
            {
                SqlDataReader dataItems = Connection.Query($"INSERT INTO [dbo].[Items] (Name, Price, Description) OUTPUT Inserted.Id " +
                    $"VALUES (N'{this.Name}', {this.Price}, N'{this.Description}')", out connection);
                dataItems.Read();
                this.Id = dataItems.GetInt32(0);
            }
            else
            {
                Connection.Query($"UPDATE [dbo].[Items] SET Name = N'{this.Name}', Price = {this.Price}, Description = N'{this.Description}'," +
                    $" IdCategory = {this.Category.Id} WHERE Id = {this.Id}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.Main);
        }

        /// <summary> Метод удаления
        public void Delete()
        {
            SqlConnection connection;
            Connection.Query($"DELETE FROM [dbo].[Items] WHERE Id = {this.Id}", out connection);
            Connection.CloseConnection(connection);
        }

        /// <summary> Команда для редактирования
        public RelayCommand OnEdit
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    // Открываем старницу редактирования, передавая весь контекст
                    MainWindow.init.frame.Navigate(new View.Add(this));
                });
            }
        }

        /// <summary> Команда для сохранения
        public RelayCommand OnSave
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    // Обновляем категорию модели
                    // Поскольку через привязку к Combobox, именяется только идентификатор
                    Category = CategorysContext.AllCategorys().Where(x => x.Id == this.Category.Id).First();
                    Save();
                });
            }
        }

        /// <summary> Команда удаления
        public RelayCommand OnDelete
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
