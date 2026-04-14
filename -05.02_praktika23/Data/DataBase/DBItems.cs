using MySql.Data.MySqlClient;
using praktika22.Data.Common;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;

namespace praktika22.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categorys> Categorys = new DBCategory().AllCategorys;

        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection mySqlConnection = Connection.mySqlOpen();
                MySqlDataReader itemsData = Connection.mySqlQuery("SELECT * FROM Shop.Items", mySqlConnection);
                while (itemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = itemsData.IsDBNull(0) ? -1 : itemsData.GetInt32(0),
                        Name = itemsData.IsDBNull(1) ? "" : itemsData.GetString(1),
                        Description = itemsData.IsDBNull(2) ? "" : itemsData.GetString(2),
                        Img = itemsData.IsDBNull(3) ? "" : itemsData.GetString(3),
                        Price = itemsData.IsDBNull(4) ? -1 : itemsData.GetInt32(4),
                        Category = itemsData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == itemsData.GetInt32(5)).First()
                    });
                }
                mySqlConnection.Close();
                return items;
            }
        }

        public IEnumerable<Items> FindItems(string text)
        {
            List<Items> items = new List<Items>();

            MySqlConnection mySqlConnection = Connection.mySqlOpen();
            MySqlDataReader itemsData = Connection.mySqlQuery($"SELECT * FROM Shop.Items WHERE `Name` LIKE '%{text}%'", mySqlConnection);
            while (itemsData.Read())
            {
                items.Add(new Items()
                {
                    Id = itemsData.GetInt32(0),
                    Name = itemsData.IsDBNull(1) ? "" : itemsData.GetString(1),
                    Description = itemsData.IsDBNull(2) ? "" : itemsData.GetString(2),
                    Img = itemsData.IsDBNull(3) ? "" : itemsData.GetString(3),
                    Price = itemsData.IsDBNull(4) ? -1 : itemsData.GetInt32(4),
                    Category = itemsData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == itemsData.GetInt32(5)).First()
                });
            }
            mySqlConnection.Close();
            return items;
        }
    }
}
