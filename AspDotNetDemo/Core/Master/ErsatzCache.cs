using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstractions.Interface;
using Clio.Demo.Util.Telemetry.Seri;
using System.Collections;

namespace Clio.Demo.Core7.Pattern
{
    public sealed class ErsatzCache<T> : Dictionary<int, T>, IEntityCache<T> where T : class, IEntity
    {
        private object _lock => (this as ICollection).SyncRoot;

        private DateTime Hydrated { get; set; }
        public TimeSpan Duration { get; set; }

        public void Flush()
        {
            try
            {
                lock (_lock)
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }

        public void Hydrate(IEnumerable<T> entities)
        {
            try
            {
                lock (_lock)
                {
                    if (Count == 0)
                    {
                        foreach (T entity in entities)
                        {
                            if (ContainsKey(entity.Id))
                            {
                                throw new Exception($"Cannot hydrate cache, duplicate Id ({entity.Id}) in entities");
                            }
                            Add(entity.Id, entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Clear();
                Log.Error(this, ex);
            }
        }

        public T Get(int key)
        {
            T entity = default;
            try
            {
                lock (_lock)
                {
                    TryGetValue(key, out entity);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return entity;
        }

        public IEnumerable<T> Get()
        {
            IEnumerable<T> entities = new List<T>();
            try
            {
                lock (_lock)
                {
                    entities = Values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return entities;
        }

        public void Put(T entity)
        {
            try
            {
                lock (_lock)
                {
                    if (ContainsKey(entity.Id))
                    {
                        throw new Exception($"Cannot put into cache, duplicate Id ({entity.Id}) in entities");
                    }
                    Add(entity.Id, entity);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }
    }
}
