using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Util.Telemetry.NLog;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clio.Demo.Core.Component.Gateway
{
    public sealed class SqlDapperGateway : ISqlGateway
    {
        public async Task<IEnumerable<T>> Read<T>(string query, string connectionString) where T : class, IEntity, new()
        {
            IEnumerable<T> data = new List<T>(); // init to empty
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    data = await connection.QueryAsync<T>(query);
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
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    affectedRows = await connection.ExecuteAsync(query);
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
            V result = default(V);
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    result = await connection.ExecuteScalarAsync<V>(query);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return result;
        }

        public async Task Insert<T>(T entity, string connectionString) where T : class, IEntity
        {
            await Task.CompletedTask;
        }

        public async Task Update<T>(T entity, string connectionString) where T : class, IEntity
        {
            await Task.CompletedTask;
        }

        public async Task Delete<T>(T entity, string connectionString) where T : class, IEntity
        {
            await Task.CompletedTask;
        }

        public async Task<int> ExecProc<T>(T entity, string proc, string connectionString) where T : class, IEntity
        {
            int identity = -1;
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    identity = await connection.ExecuteScalarAsync<int>(proc);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return identity;
        }
    }
}
