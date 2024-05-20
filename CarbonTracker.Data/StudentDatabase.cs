using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CarbonTracker.Data
{
    public class CarbonTrackerDb
    {
        private readonly string _connectionString;

        public CarbonTrackerDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<string> GetUserNames()
        {
            var names = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM users";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var name = dataReader.GetString(0);
                        names.Add(name);
                    }
                }
            }
            return names;
        }

        public void AddUserName(string userName)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO users (name) VALUES (@Name)";
                command.Parameters.AddWithValue("@Name", userName);
                command.ExecuteNonQuery();
            }
        }
    }
}

