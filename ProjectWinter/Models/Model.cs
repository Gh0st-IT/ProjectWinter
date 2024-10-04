using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Npgsql;

namespace ProjectWinter.Models
{
    public class Model
    {
        private readonly string _connectionString;

        public Model()
        {
            _connectionString = "Host=10.248.1.152;Database=PROJECT_WINTER;Username=postgres;Password=1234";
        }

        public void CheckConnection()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connected successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection Failed: {e.Message}");
            }
        }

        private NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public void PostgresqlInsert(string tableName, Dictionary<string, object> associativeData)
        {
            var columns = string.Join(", ", associativeData.Keys);
            var placeholders = string.Join(", ", associativeData.Keys.Select(k => $"@{k}"));

            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({placeholders})";

            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                foreach (var kvp in associativeData)
                {
                    cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public List<Dictionary<string, object>> PostgresqlSelect(
            string tableName,
            string columns = "*",
            string condition = "",
            int limit = 50,
            int offset = 0,
            string order = "")
        {
            var sql = $"SELECT {columns} FROM {tableName}";

            if (!string.IsNullOrEmpty(condition))
            {
                sql += $" WHERE {condition}";
            }

            if (!string.IsNullOrEmpty(order))
            {
                sql += $" ORDER BY {order}";
            }

            if (limit != 0)
            {
                sql += $" LIMIT {limit}";
            }

            if (offset != 0)
            {
                sql += $" OFFSET {offset}";
            }

            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                var results = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    results.Add(row);
                }
                return results;
            }
        }

        public Dictionary<string, object> PostgresqlUpdate(
            string tableName,
            Dictionary<string, object> associativeData,
            string condition,
            List<string> columnException = null)
        {
            if (columnException == null)
            {
                columnException = new List<string>();
            }
            var setClause = string.Join(", ", associativeData
                .Where(kvp => !columnException.Contains(kvp.Key))
                .Select(kvp => $"\"{kvp.Key}\" = @{kvp.Key}"));

            var sql = $"UPDATE {tableName} SET {setClause}";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += $" WHERE {condition}";
            }

            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                foreach (var kvp in associativeData.Where(kvp => !columnException.Contains(kvp.Key)))
                {
                    cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                }

                cmd.ExecuteNonQuery();
            }

            return associativeData;
        }

        public void PostgresqlDelete(string tableName, string where)
        {
            var sql = $"DELETE FROM {tableName} WHERE {where}";

            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public Dictionary<string, object> DataTablesGet(
            string tableName,
            int limit,
            int offset,
            List<string> searchColumns = null,
            string searchValue = "",
            Dictionary<string, object> searchCustomConditions = null,
            string orderColumn = "",
            string orderDirection = "ASC",
            List<string> fixedConditions = null)
        {
            if (searchColumns == null)
            {
                searchColumns = new List<string> { "*" };
            }
            if (searchCustomConditions == null)
            {
                searchCustomConditions = new Dictionary<string, object>();
            }
            if (fixedConditions == null)
            {
                fixedConditions = new List<string>();
            }

            var whereConditions = new List<string>(fixedConditions);
            var parameters = new Dictionary<string, object>();

            // Handle custom conditions
            if (searchCustomConditions.ContainsKey("criteria") && searchCustomConditions["criteria"] is List<Dictionary<string, object>> criteria)
            {
                var customConditions = new List<string>();
                for (int i = 0; i < criteria.Count; i++)
                {
                    var condition = criteria[i];
                    var placeholder = $"@value{i}";

                    switch (condition["condition"].ToString().ToLower())
                    {
                        case "equals":
                        case "=":
                            customConditions.Add($"{condition["origData"]} = {placeholder}");
                            parameters[placeholder] = condition["value1"];
                            break;
                        case "not":
                        case "!=":
                            customConditions.Add($"{condition["origData"]} != {placeholder}");
                            parameters[placeholder] = condition["value1"];
                            break;
                            // ... (other cases similar to PHP version)
                    }
                }

                if (customConditions.Any())
                {
                    whereConditions.Add($"({string.Join($" {searchCustomConditions["logic"]} ", customConditions)})");
                }
            }

            // Handle search value
            if (!string.IsNullOrEmpty(searchValue) && !searchColumns.Contains("*"))
            {
                var searchConditions = new List<string>();
                for (int i = 0; i < searchColumns.Count; i++)
                {
                    var placeholder = $"@search{i}";
                    searchConditions.Add($"{searchColumns[i]} LIKE {placeholder}");
                    parameters[placeholder] = $"%{searchValue}%";
                }
                if (searchConditions.Any())
                {
                    whereConditions.Add($"({string.Join(" OR ", searchConditions)})");
                }
            }

            var sql = $"SELECT * FROM {tableName}";
            if (whereConditions.Any())
            {
                sql += $" WHERE {string.Join(" AND ", whereConditions)}";
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                sql += $" ORDER BY {orderColumn} {orderDirection}";
            }

            sql += $" LIMIT @limit OFFSET @offset";
            parameters["@limit"] = limit;
            parameters["@offset"] = offset;

            List<Dictionary<string, object>> data;
            long totalRecords;
            long recordsFiltered;

            using (var conn = GetConnection())
            {
                // Fetch data
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        data = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            data.Add(row);
                        }
                    }
                }

                // Count total records
                using (var cmd = new NpgsqlCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                {
                    totalRecords = (long)cmd.ExecuteScalar();
                }

                // Count filtered records
                if (whereConditions.Any())
                {
                    var countFilteredSql = $"SELECT COUNT(*) FROM {tableName} WHERE {string.Join(" AND ", whereConditions)}";
                    using (var cmd = new NpgsqlCommand(countFilteredSql, conn))
                    {
                        foreach (var param in parameters.Where(p => !p.Key.StartsWith("@limit") && !p.Key.StartsWith("@offset")))
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        recordsFiltered = (long)cmd.ExecuteScalar();
                    }
                }
                else
                {
                    recordsFiltered = totalRecords;
                }
            }

            return new Dictionary<string, object>
            {
                ["draw"] = 0, // You might want to pass this value from the client
                ["recordsTotal"] = totalRecords,
                ["recordsFiltered"] = recordsFiltered,
                ["data"] = data
            };
        }

        // ... (Other methods like DataTablesGetJoinedTables, PostgresqlSelectJoinedTables, etc. can be implemented similarly)

        public bool PostgresqlCheckExists(string tableName, string condition = "", bool debug = false)
        {
            var result = PostgresqlSelect(tableName, "*", condition, 1);

            if (debug)
            {
                Console.WriteLine("Query Result:");
                foreach (var row in result)
                {
                    foreach (var keyValuePair in row)
                    {
                        Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
                    }
                    Console.WriteLine(); // For spacing between rows
                }
            }

            return result.Any();
        }
    }
}