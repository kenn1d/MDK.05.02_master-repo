using MySql.Data.MySqlClient;
using praktika20.Classes.Common;
using praktika20.Models;
using System;
using System.Collections.Generic;

namespace praktika20.Classes
{
    public class WorkContext : Work
    {
        public WorkContext(int id, int idDescipline, int idType, DateTime date, string name, int semester) : base(id, idDescipline, idType, date, name, semester) { }

        public static List<WorkContext> AllWorks()
        {
            List<WorkContext> allWorks = new List<WorkContext>();

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader BDWorks = Connection.Query("SELECT * FROM `Work` ORDER BY `Date`", connection);

            while (BDWorks.Read())
            {
                allWorks.Add(new WorkContext(
                    BDWorks.GetInt32(0),
                    BDWorks.GetInt32(1),
                    BDWorks.GetInt32(2),
                    BDWorks.GetDateTime(3),
                    BDWorks.GetString(4),
                    BDWorks.GetInt32(5)
                    ));
            }

            Connection.CloseConnection(connection);

            return allWorks;
        }
    }
}
