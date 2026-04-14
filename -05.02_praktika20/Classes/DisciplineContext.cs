using MySql.Data.MySqlClient;
using praktika20.Classes.Common;
using praktika20.Models;
using System.Collections.Generic;

namespace praktika20.Classes
{
    public class DisciplineContext : Discipline
    {
        public DisciplineContext(int id, string name, int idGroup) : base(id, name, idGroup) { }

        public static List<DisciplineContext> AllDisciplines()
        {
            List<DisciplineContext> allDisciplines = new List<DisciplineContext>();

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader BDDisciplines = Connection.Query("SELECT * FROM `Discipline` ORDER BY `Name`", connection);

            while (BDDisciplines.Read())
            {
                allDisciplines.Add(new DisciplineContext(
                    BDDisciplines.GetInt32(0),
                    BDDisciplines.GetString(1),
                    BDDisciplines.GetInt32(2)
                    ));
            }

            Connection.CloseConnection(connection);

            return allDisciplines;
        }
    }
}
