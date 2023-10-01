using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
    public class Product : IEntity
    {
        public int     ProductID { get; set; }
        public int     SupplierID { get; set; }
        public string  ProductName { get; set; }
        public decimal OrderUnitPrice { get; set; }
        public short   Quantity { get; set; }
        public float   Discount { get; set; }
        public string  QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short   UnitsInStock { get; set; }
        public short   UnitsOnOrder { get; set; }
        public short   ReorderLevel { get; set; }
        public bool    Discontinued { get; set; }

        public Supplier Supplier { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Code { get; set; }
        [JsonIgnore]
        public string Lookup { get; set; }
        [JsonIgnore]
        public string Name => ProductName;

        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{ProductID} {ProductName}";
        }
    }
}
