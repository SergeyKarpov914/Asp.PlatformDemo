using System;

namespace Clio.Demo.Abstraction.Interface
{
    public enum KeyType : int
    { 
        EntityId = 0,
        EntityCode,
        EntityName,
        EntityLookup,
        EntityCreated,
        EntityUpdated,
        Column    
    }

    public interface IEntity
    {
        int    Id     { get; }
        string Code   { get; }
        string Name   { get; }
        string Lookup { get; }

        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
