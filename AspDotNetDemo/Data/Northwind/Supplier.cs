using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
    public class Supplier : IEntity
    {
        public int    SupplierID   { get; set; }
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
        public string HomePage     { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Code => SupplierID.ToString();
        [JsonIgnore]
        public string Lookup => SupplierID.ToString();
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{SupplierID} {CompanyName}";
        }
    }
}
