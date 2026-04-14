using praktika15.Classes.Common;
using praktika15.Interfaces;
using System.Collections.Generic;
using System.Data.OleDb;

namespace praktika15.Classes
{
    public class UserContext : Models.User, IUser
    {
        public List<UserContext> AllUsers()
        {
            List<UserContext> allUsers = new List<UserContext>();

            OleDbConnection connection = DBConnection.Connection();
            OleDbDataReader dataUsers = DBConnection.Query("SELECT * FROM [Ответственные]", connection);
            while (dataUsers.Read())
            {
                allUsers.Add(new UserContext()
                {
                    Id = dataUsers.GetInt32(0),
                    Name = dataUsers.GetString(1)
                });
            }
            DBConnection.CloseConnection(connection);

            return allUsers;
        }

        public void Delete()
        {
            OleDbConnection connection = DBConnection.Connection();
            DBConnection.Query(
                    $"DELETE FROM [Ответственные] WHERE [Код] = {this.Id}", connection);
            DBConnection.CloseConnection(connection);
        }

        public void Save(bool Update = false)
        {
            OleDbConnection connection = DBConnection.Connection();
            if (Update)
            {
                DBConnection.Query(
                    $"UPDATE " +
                        $"[Ответственные] " +
                    $"SET " +
                        $"[Наименование] = '{this.Name}' " +
                    $"WHERE " +
                        $"[Код] = {this.Id}", connection);
            }
            else
            {
                DBConnection.Query(
                    $"INSERT INTO " +
                        $"[Ответственные] ( " +
                            $"[Наименование])" +
                    $" VALUES (" +
                        $"'{this.Name}')", connection);
            }
            DBConnection.CloseConnection(connection);
        }
    }
}
