using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WpfApp1.Classes.Common;
using WpfApp1.Model;

namespace WpfApp1.Classes
{
    public class BiletContext : Bilet
    {
        public BiletContext(int Id, int idAfisha) : base(Id, idAfisha) { }

        public static List<BiletContext> Select()
        {
            List<BiletContext> AllBilets = new List<BiletContext>();

            string SQL = "SELECT * FROM `bilet`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllBilets.Add(new BiletContext(
                        Data.GetInt32(0),
                        Data.GetInt32(1)
                    ));
            }
            Connection.CloseConnection(connection);

            return AllBilets;
        }

        public void Add()
        {
            string SQL = $"INSERT INTO `bilet`(`id_afisha`) VALUES ('{this.idAfisha}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
