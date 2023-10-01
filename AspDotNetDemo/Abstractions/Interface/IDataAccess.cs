using Clio.Demo.Abstraction.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clio.Demo.Abstraction.Interface
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
