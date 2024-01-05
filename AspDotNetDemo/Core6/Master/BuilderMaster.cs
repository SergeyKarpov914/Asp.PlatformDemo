﻿using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Util;

namespace Clio.Demo.Core7.Pattern
{
    public class BuilderMaster<T> where T : class, IEntity, new()
    {
        protected readonly T _entity = new();
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
