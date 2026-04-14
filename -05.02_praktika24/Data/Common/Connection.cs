using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;

namespace praktika22.Data.Common
{
    public class Connection
    {
        readonly static string connection = "server=127.0.0.1;port=3306;database=Shop;uid=root";

        public static MySqlConnection mySqlOpen()
        {
            MySqlConnection newMySqlConnection = new MySqlConnection(connection);
            newMySqlConnection.Open();
            return newMySqlConnection;
        }

        public static MySqlDataReader mySqlQuery(string query, MySqlConnection connection)
        {
            MySqlCommand newMySqlCommand = new MySqlCommand(query, connection);
            return newMySqlCommand.ExecuteReader();
        }

        public static void mySqlClose(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
