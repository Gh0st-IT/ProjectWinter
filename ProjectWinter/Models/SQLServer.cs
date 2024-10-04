using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq; // Ensure you have the appropriate using directive

namespace ProjectWinter.Models
{
    internal class SQLServer
    {
        private string _connectionString;

        public SQLServer()
        {
            _connectionString = "Server=APBIPHDB23;Database=EMPLOYEE_DATA;User Id=Winter;Password=winteriscoming;";
        }

        // Method to open a connection
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Method to execute a query and return a DataTable
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        return dataTable;
                    }
                }
            }
        }

        // Method to execute a non-query command (e.g., INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string commandText, Dictionary<string, object> parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    connection.Open();
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }

        // Example method for inserting data
        public void InsertData(string tableName, Dictionary<string, object> data)
        {
            var columns = string.Join(", ", data.Keys);
            var parameters = string.Join(", ", data.Keys.Select(k => $"@{k}"));
            var commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            ExecuteNonQuery(commandText, data);
        }

        // Example method for reading data
        public DataTable ReadData(string tableName, string condition = "")
        {
            var query = $"SELECT * FROM {tableName}" + (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");
            return ExecuteQuery(query);
        }

        // Example method for updating data
        public void UpdateData(string tableName, Dictionary<string, object> data, string condition)
        {
            var setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));
            var commandText = $"UPDATE {tableName} SET {setClause} WHERE {condition}";

            ExecuteNonQuery(commandText, data);
        }

        // Example method for deleting data
        public void DeleteData(string tableName, string condition)
        {
            var commandText = $"DELETE FROM {tableName} WHERE {condition}";
            ExecuteNonQuery(commandText);
        }
    }
}
