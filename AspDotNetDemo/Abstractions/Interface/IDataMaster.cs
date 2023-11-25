using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clio.Demo.Abstraction.Interface.Mk2
{
    public interface IDataAccess<T> where T : class, IEntity
    {
        Task                 Create(T entity);
        Task<IEnumerable<T>> Read(string clause = null);
        Task                 Update(T entity);
        Task                 Delete(T entity);
    }
}
