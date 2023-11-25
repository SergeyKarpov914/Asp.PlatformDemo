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

        void ISqlGateway.Insert<T>(T entity, string table, string connectionString, IEnumerable<string> columns)
        {
            throw new NotImplementedException();
        }

        public void Insert(string connectionString, string insertQuery)
        {
            throw new NotImplementedException();
        }

        int ISqlGateway.ExecProc<T>(T entity, string proc, string connectionString, IEnumerable<string> columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Columns(string table, string connectionString, ColumnSet columnSet = ColumnSet.All)
        {
            throw new NotImplementedException();
        }
    }
}
