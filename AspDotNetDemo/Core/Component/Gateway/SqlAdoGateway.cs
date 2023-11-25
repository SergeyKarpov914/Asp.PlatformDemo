using Clio.Demo.Abstraction.Data;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Extension;
using Clio.Demo.Util;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clio.Demo.Core.Gateway
{
    public class SqlAdoGateway : ISqlGateway
    {
        public async Task<IEnumerable<T>> Read<T>(string query, string connectionString) where T : class, IEntity, new()
        {
            List<T> data = new List<T>();
            try
            {
                if (!connectionString.StartsWith("Server"))
                {
                    connectionString = connectionString.Decode();
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (true == await reader.ReadAsync())
                            {
                                data.Add(reader.MapTo<T>());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return data;
        }

        public async Task<int> Execute(string query, string connectionString)
        {
            int affectedRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        affectedRows = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return affectedRows;
        }

        public async Task<V> ExecuteScalar<V>(string query, string connectionString)
        {
            V result = default;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        result = (V)await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return result;
        }

        public int ExecProc<T>(T entity, string proc, string connectionString, IEnumerable<string> columns) where T : class, IEntity
        {
            int identity = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Sql.ExecuteProc(entity, columns, proc), connection))
                    {
                        identity = (int)(decimal)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return identity;
        }

        public async Task Update<T>(T entity, string table, string connectionString, IEnumerable<string> columns) where T : class, IEntity
        {
            try
            {
                await Execute(Sql.UpdateQuery(entity, columns, table), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public void Insert<T>(T entity, string table, string connectionString, IEnumerable<string> columns) where T : class, IEntity
        {
            try
            {
                insertRow(connectionString, Sql.InsertQuery(entity, columns, table));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public void Insert(string connectionString, string insertQuery)
        {
            try
            {
                insertRow(connectionString, insertQuery);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public async Task Delete<T>(T entity, string table, string connectionString) where T : class, IEntity
        {
            try
            {
                string clause = $"Id = {entity.Id}";

                await Execute(Sql.DeleteQuery(table, clause), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public async Task Delete(Key key, string table, string connectionString)
        {
            try
            {
                string clause = $"{key.Column} = {key.SqlValue}";

                await Execute(Sql.DeleteQuery(table, clause), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        private void insertRow(string connectionString, string insertQuery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<string> Columns(string table, string connectionString, ColumnSet columnSet = ColumnSet.All)
        {
            IEnumerable<string> columns = null;
            try
            {
                columns = tableColumns(table, connectionString, columnSet);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return columns;
        }

        private IEnumerable<string> tableColumns(string table, string connectionString, ColumnSet columnSet = ColumnSet.All)
        {
            List<string> columns = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {table} WHERE 1 = 0", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (excludeColumn(column, columnSet))
                        {
                            continue;
                        }
                        columns.Add(column.ColumnName);
                    }
                }
            }
            return columns;
        }

        private bool excludeColumn(DataColumn column, ColumnSet columnSet)
        {
            switch (columnSet)
            {
                case ColumnSet.All:
                    return false;
                case ColumnSet.Insert:
                    return column.ColumnName.Equals("id", StringComparison.OrdinalIgnoreCase); //if (column.ReadOnly || column.AutoIncrement)
            }
            return false;
        }
    }
}


