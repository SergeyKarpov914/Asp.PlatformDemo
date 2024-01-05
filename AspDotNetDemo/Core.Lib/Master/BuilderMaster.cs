using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Util;
using System;
using System.Collections.Generic;

namespace Clio.Demo.Core.Lib.Pattern
{
    public class BuilderMaster<T> where T : class, IEntity, new()
    {
        protected readonly T _entity = new T();
        protected readonly List<Exception> _exceptions = new List<Exception>();

        public T Build()
        {
            try
            {
                validate();
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return _entity;
        }

        public virtual BuilderMaster<T> WithDefaults()
        {
            _entity.Created = DateTime.Now;
            _entity.Updated = DateTime.Now;

            return this;
        }

        protected virtual void validate()
        {
        }
    }
}
