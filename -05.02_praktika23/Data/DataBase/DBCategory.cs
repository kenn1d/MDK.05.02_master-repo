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
    }
}
