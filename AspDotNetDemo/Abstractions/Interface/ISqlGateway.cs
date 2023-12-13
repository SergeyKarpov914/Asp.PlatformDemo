using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clio.Demo.Abstraction.Interface
{
    public enum ColumnSet { All, Insert }

    public interface ISqlGateway
    {
        Task<IEnumerable<T>> Read<T>          (string query, string connectionString) where T : class, IEntity, new();
        Task<int>            Execute          (string query, string connectionString);
        Task<V>              ExecuteScalar<V> (string query, string connectionString);
        
        Task                 Insert<T>        (T entity, string connectionString) where T : class, IEntity;
        Task                 Update<T>        (T entity, string connectionString) where T : class, IEntity;
        Task                 Delete<T>        (T entity, string connectionString) where T : class, IEntity;
        Task<int>            ExecProc<T>      (T entity, string proc, string connectionString) where T : class, IEntity;
    }
}
