using System;
using System.Collections.Generic;

namespace Clio.Demo.Abstraction.Data
{
    public class Key
    {
        public string Column { get; set; }
        public virtual string SqlValue  { get; set; }
    }

    public class Key<T, TEnum> : Key where TEnum : Enum
    {
        public T Value { get; set; }
        public TEnum ValueType { get; set; }
    }

    public class MultiKey
    {
        public string Column { get; set; }
        public virtual IEnumerable<string> SqlValues { get; set; }
    }

    public class MultiKey<T, TEnum> : MultiKey where TEnum : Enum
    {
        public IEnumerable<T> Values { get; set; }
        public TEnum ValueType { get; set; }
    }
}
