using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Extension;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<int> ExecProc<T>(T entity, string proc, string connectionString) where T : class, IEntity
        {
            int identity = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(proc, connection))
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

        public async Task Update<T>(T entity, string connectionString) where T : class, IEntity
        {
            try
            {
                await Execute(entity.UpdateQuery(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public async Task Insert<T>(T entity, string connectionString) where T : class, IEntity
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(entity.InsertQuery(), connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }

        public async Task Delete<T>(T entity, string connectionString) where T : class, IEntity
        {
            try
            {
                await Execute(entity.DeleteQuery(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
        }
    }
}


