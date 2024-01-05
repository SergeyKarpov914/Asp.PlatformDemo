using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Core7.Pattern
{
    public class EntityMaster : IEntity
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public string Code { get; set; }
        [JsonIgnore] public string Name { get; set; }
        [JsonIgnore] public string Lookup { get; set; }

        [JsonIgnore] public DateTime Created { get; set; }
        [JsonIgnore] public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name} ({Id}) {Name}";
        }
    }
}
