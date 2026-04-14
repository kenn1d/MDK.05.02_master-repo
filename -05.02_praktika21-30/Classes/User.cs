using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace praktika21_30_.Classes
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public byte[] Image = new byte[0];

        public DateTime DateUpdate { get; set; }

        public DateTime DateCreate { get; set; }

        public CorrectLogin HandlerCorrectLogin;

        public InCorrectLogin HandlerInCorrectLogin;

        public delegate void CorrectLogin();

        public delegate void InCorrectLogin();

        public void GetUserLogin(string Login)
        {
            this.Id = 1;
            this.Login = String.Empty;
            this.Password = String.Empty;
            this.Name = String.Empty;
            this.Image = new byte[0];

            MySqlConnection mySqlConnection = WorkingDB.OpenConnection();

            if (WorkingDB.OpenConnection(mySqlConnection))
            {
                MySqlDataReader userQuery = WorkingDB.Query($"SELECT * FROM `users` WHERE `Login` = '{Login}'"
                    , mySqlConnection);
                if (userQuery.HasRows)
                {
                    userQuery.Read();
                    this.Id = userQuery.GetInt32(0);
                    this.Login = userQuery.GetString(1);
                    this.Password = userQuery.GetString(2);
                    this.Name = userQuery.GetString(3);
                    if (!userQuery.IsDBNull(4))
                    {
                        this.Image = new byte[63 * 1024];
                        userQuery.GetBytes(4, 0, Image, 0, Image.Length);
                    }
                    this.DateUpdate = userQuery.GetDateTime(5);
                    this.DateCreate = userQuery.GetDateTime(6);
                    HandlerCorrectLogin.Invoke();
                }
                else HandlerInCorrectLogin.Invoke();
            }
            else HandlerInCorrectLogin.Invoke();

            WorkingDB.CloseConnection(mySqlConnection);
        }

        public void SetUser()
        {
            MySqlConnection mySqlConnection = WorkingDB.OpenConnection();

            if (WorkingDB.OpenConnection(mySqlConnection))
            {
                MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO `users`(`Login`, " +
                    "`Password`, `Name`, `Image`, `DateUpdate`, `DateCreate`) " +
                    "VALUES (@Login, @Password, @Name, @Image, @DateUpdate, @DateCreate)", mySqlConnection);

                mySqlCommand.Parameters.AddWithValue("Login", this.Login);
                mySqlCommand.Parameters.AddWithValue("Password", this.Password);
                mySqlCommand.Parameters.AddWithValue("Name", this.Name);
                mySqlCommand.Parameters.AddWithValue("Image", this.Image);
                mySqlCommand.Parameters.AddWithValue("DateUpdate", this.DateUpdate);
                mySqlCommand.Parameters.AddWithValue("DateCreate", this.DateCreate);

                mySqlCommand.ExecuteNonQuery();
            }
            WorkingDB.CloseConnection(mySqlConnection);
        }

        public void CreateNewPassword()
        {
            if (Login != String.Empty)
            {
                Password = GeneratePass();
                MySqlConnection mySqlConnection = WorkingDB.OpenConnection();
                if (WorkingDB.OpenConnection(mySqlConnection))
                {
                    WorkingDB.Query($"UPDATE `users` SET `Password`='{this.Password}' WHERE " +
                        $"`Login` = '{this.Login}'", mySqlConnection);
                }
                WorkingDB.CloseConnection(mySqlConnection);
                SendMail.SendMessage($"Your acccount password has been changed.\nNew password: " +
                    $"{this.Password}", this.Login);
            }
        }

        public string GeneratePass()
        {
            List<Char> NewPassword = new List<char>();
            Random rnd = new Random();
            char[] ArrNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] ArrSymbols = { '|', '-', '_', '!', '@', '#', '$', '%', '&', '*', '=', '+' };
            char[] ArrUppercase = { 'q', 'w', 'e', 'r', 't', 'y', 's', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h',
                'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };

            for (int i = 0; i < 1; i++) NewPassword.Add(ArrNumbers[rnd.Next(0, ArrNumbers.Length)]);

            for (int i = 0; i < 1; i++) NewPassword.Add(ArrSymbols[rnd.Next(0, ArrSymbols.Length)]);
            
            for (int i = 0; i < 2; i++) NewPassword.Add(char.ToUpper(ArrUppercase[rnd.Next(0, ArrUppercase.Length)]));

            for (int i = 0; i < 6; i++) NewPassword.Add(ArrUppercase[rnd.Next(0, ArrUppercase.Length)]);

            for (int i = 0; i < NewPassword.Count; i++)
            {
                int RandomSymbo = rnd.Next(0, NewPassword.Count);
                char Symbol = NewPassword[RandomSymbo];
                NewPassword[RandomSymbo] = NewPassword[i];
                NewPassword[i] = Symbol;
            }
            string NPassword = "";
            for (int i = 0; i < NewPassword.Count; i++)
            {
                NPassword += NewPassword[i];
            }
            return NPassword;
        }
    }
}
