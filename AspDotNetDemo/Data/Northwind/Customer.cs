using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
    public class Customer : IEntity
    {
        public int    CustomerID   { get; set; }
        public string CompanyName  { get; set; }
        public string ContactName  { get; set; }
        public string ContactTitle { get; set; }
        public string Address      { get; set; }
        public string City         { get; set; }
        public string Region       { get; set; }
        public string PostalCode   { get; set; }
        public string Country      { get; set; }
        public string Phone        { get; set; }
        public string Fax          { get; set; }

        [JsonIgnore]
        public int Id => CustomerID;
        [JsonIgnore]
        public string Code => ContactName;
        [JsonIgnore]
        public string Lookup => CustomerID.ToString();
        [JsonIgnore]
        public string Name => CompanyName;

        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
