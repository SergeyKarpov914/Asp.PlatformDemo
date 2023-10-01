using Clio.Demo.Core.Interface;

namespace Clio.Demo.Core.Interface
{
    public enum ColumnSet { All, Insert }

    public interface ISQLClient
    {
        Task<IEnumerable<T>> Read<T>          (string query, string connectionString) where T : class, IEntity, new();
        Task                 Execute          (string query, string connectionString);
        Task<V>              ExecuteScalar<V> (string query, string connectionString) where V : class, IEntity;
        void                 Insert<T>     (T entity, string table, string connectionString, IEnumerable<string> columns) where T : class, IEntity;
        void                 Insert        (string connectionString, string insertQuery);
        int                  ExecProc<T>(T entity, string proc, string connectionString, IEnumerable<string> columns) where T : class, IEntity;

        IEnumerable<string> Columns(string table, string connectionString, ColumnSet columnSet = ColumnSet.All);
    }
}
