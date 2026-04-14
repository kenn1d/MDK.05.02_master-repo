using MySql.Data.MySqlClient;
using praktika22.Data.Common;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using System.ComponentModel;

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

        public int Add(Items Item)
        {
            MySqlConnection sql = Connection.mySqlOpen();
            Connection.mySqlQuery(
                $"INSERT INTO `Items`(`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}','{Item.Description}','{Item.Img}','{Item.Price}','{Item.Category.Id}');",
                sql);
            sql.Close();

            // для редиректа (получаем id нового элмента)
            int IdItem = -1;
            sql= Connection.mySqlOpen();
            MySqlDataReader mySqlDataReaderItem = Connection.mySqlQuery(
                $"SELECT `Id` FROM `Items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Category.Id};",
                sql);
            if (mySqlDataReaderItem.HasRows)
            {
                mySqlDataReaderItem.Read();
                IdItem = mySqlDataReaderItem.GetInt32(0);
            }
            sql.Close();
            return IdItem;
        }

        public void Delete(int idItem)
        {
            MySqlConnection sql = Connection.mySqlOpen();
            Connection.mySqlQuery(
                $"DELETE FROM `Items` WHERE `Id` = {idItem};", sql);
            sql.Close();
        }

        public void Update(Items Item)
        {
            MySqlConnection sql = Connection.mySqlOpen();
            Connection.mySqlQuery(
                $"UPDATE `Items` SET `Name`='{Item.Name}',`Description`='{Item.Description}',`Img`='{Item.Img}',`Price`={Item.Price},`IdCategory`={Item.Category.Id} WHERE `Id` = {Item.Id}", sql);
            sql.Close();
        }
    }
}
