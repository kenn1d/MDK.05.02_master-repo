using MySql.Data.MySqlClient;
using praktika20.Classes.Common;
using praktika20.Models;
using System.Collections.Generic;

namespace praktika20.Classes
{
    public class GroupContext : Group
    {
        public GroupContext(int id, string name) : base(id, name) { }

        public static List<GroupContext> AllGroups()
        {
            List<GroupContext> allGroups = new List<GroupContext>();

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader BDGroups = Connection.Query("SELECT * FROM `Group` ORDER BY `Name`", connection);

            while (BDGroups.Read())
            {
                allGroups.Add(new GroupContext(
                    BDGroups.GetInt32(0),
                    BDGroups.GetString(1)
                    ));
            }

            Connection.CloseConnection(connection);

            return allGroups;
        }
    }
}
