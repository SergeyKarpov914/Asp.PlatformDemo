using System.Text.Json.Serialization;

namespace Clio.Demo.Core.Component.Master.Pattern
{
    public class EntityMaster
    {
        [JsonIgnore] public int    Id     { get; set; }
        [JsonIgnore] public string Code   { get; set; }
        [JsonIgnore] public string Name   { get; set; }
        [JsonIgnore] public string Lookup { get; set; }

        [JsonIgnore] public DateTime Created { get; set; }
        [JsonIgnore] public DateTime Updated { get; set; }
    }
}
