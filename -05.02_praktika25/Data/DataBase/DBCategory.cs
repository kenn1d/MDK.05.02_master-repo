using MySql.Data.MySqlClient;
using praktika22.Data.Common;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;

namespace praktika22.Data.DataBase
{
    public class DBCategory : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                List<Categorys> categorys = new List<Categorys>();
                MySqlConnection mySqlConnection = Connection.mySqlOpen();
                MySqlDataReader categorysData = Connection.mySqlQuery("SELECT * FROM Shop.Categorys", mySqlConnection);
                while (categorysData.Read())
                {
                    categorys.Add(new Categorys()
                    {
                        Id = categorysData.IsDBNull(0) ? -1 : categorysData.GetInt32(0),
                        Name = categorysData.IsDBNull(1) ? null : categorysData.GetString(1),
                        Description = categorysData.IsDBNull(2) ? null : categorysData.GetString(2),
                    });
                }
                return categorys;
            }
        }

        public void Add(string Name, string Desc)
        {
            MySqlConnection connection = Connection.mySqlOpen();
            Connection.mySqlQuery($"INSERT INTO `Categorys`(`Name`, `Description`) VALUES ('{Name}','{Desc}')", connection);
            connection.Close();
        }

        public void Delete(int Id)
        {
            MySqlConnection connection = Connection.mySqlOpen();
            Connection.mySqlQuery($"DELETE FROM `Categorys` WHERE `Id` = {Id}", connection);
            connection.Close();
        }

        public void Update(Categorys category)
        {
            MySqlConnection connection = Connection.mySqlOpen();
            Connection.mySqlQuery($"UPDATE `Categorys` SET `Name`='{category.Name}',`Description`='{category.Description}' WHERE `Id` = {category.Id}", connection);
            connection.Close();
        }
    }
}
