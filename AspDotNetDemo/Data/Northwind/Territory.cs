using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using System.Text.Json.Serialization;

namespace OrderService.Data
{
    public class Territory : IEntity
	{
		public int    EmployeeID            { get; set; }
		public string TerritoryID			{ get; set; }
		public string TerritoryDescription	{ get; set; }
		public string RegionDescription	    { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Code => TerritoryID;
        [JsonIgnore]
        public string Lookup => TerritoryID;
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }
    }
}
