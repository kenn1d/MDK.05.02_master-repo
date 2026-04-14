using MySql.Data.MySqlClient;
using praktika20.Classes.Common;
using praktika20.Models;
using System;
using System.Collections.Generic;

namespace praktika20.Classes
{
    public class StudentContext : Student
    {
        public StudentContext(int id, string firstName, string lastName, int idGroup, bool expelled, DateTime dateExpelled) : base(id, firstName, lastName, idGroup, expelled, dateExpelled) { }

        public static List<StudentContext> AllStudents()
        {
            List<StudentContext> allStudents = new List<StudentContext>();

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader BDStudents = Connection.Query("SELECT * FROM `Student` ORDER BY `LastName`", connection);

            while (BDStudents.Read())
            {
                allStudents.Add(new StudentContext(
                    BDStudents.GetInt32(0),
                    BDStudents.GetString(1),
                    BDStudents.GetString(2),
                    BDStudents.GetInt32(3),
                    BDStudents.GetBoolean(4),
                    BDStudents.IsDBNull(5) ? DateTime.Now : BDStudents.GetDateTime(5)
                    ));
            }

            Connection.CloseConnection(connection);

            return allStudents;
        }
    }
}
