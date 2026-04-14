using praktika27.Classes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace praktika27.Context
{
    public class CategorysContext : Modell.Categorys
    {
        public CategorysContext(bool save = false) {
            if (save) Save(true);
        }

        /// <summary> Метод загрузки данных из БД
        public static ObservableCollection<CategorysContext> AllCategorys()
        {
            ObservableCollection<CategorysContext> allCategorys = new ObservableCollection<CategorysContext>();
            SqlConnection connection;
            SqlDataReader dataCategorys = Connection.Query("SELECT * FROM [dbo].[Categorys]", out connection);
            while (dataCategorys.Read())
            {
                allCategorys.Add(new CategorysContext()
                {
                    Id = dataCategorys.GetInt32(0),
                    Name = dataCategorys.GetString(1)
                });
            }
            Connection.CloseConnection(connection);
            return allCategorys;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataCategorys = Connection.Query($"INSERT INTO [dbo].[Categorys] (Name) OUTPUT Inserted.Id " +
                    $"VALUES (N'{this.Name}')", out connection);
                dataCategorys.Read();
                this.Id = dataCategorys.GetInt32(0);
            }
            else
            {
                Connection.Query($"UPDATE [dbo].[Categorys] SET Name = N'{this.Name}' WHERE Id = {this.Id}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(new View.MainCategory());
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query($"DELETE FROM [dbo].[Categorys] WHERE Id = {this.Id}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    // Открываем старницу редактирования, передавая весь контекст
                    MainWindow.init.frame.Navigate(new View.AddCategory(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    Save();
                });
            }
        }

        public RelayCommand OnDelete
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    Delete();
                    (MainWindow.init.MainCategory.DataContext as ViewModell.VMCategorys).Categorys.Remove(this);
                });
            }
        }
    }
}
