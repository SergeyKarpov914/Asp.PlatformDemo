using Clio.Demo.Core.Data;

namespace Clio.Demo.Core.Interface
{
    public interface IDataAccess<T> where T : class
    {
        Task                 Create (T entity);
        Task<IEnumerable<T>> ReadAll(MultiKey multiKey = null);
        Task<T>              Read   (Key key);
        Task                 Update (T entity);
        Task                 Delete (T entity);
        Task                 Delete (MultiKey multiKey);

        Task<int>            Count  (Key key);

        bool IsTest { get; set; }
    }
}
