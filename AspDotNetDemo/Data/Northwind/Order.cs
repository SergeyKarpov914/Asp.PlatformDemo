using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
    public class Order : IEntity
    {
        public int       OrderID      { get; set; }
        public int       EmployeeID   { get; set; }
        public int       CustomerID   { get; set; }
        public int       ShipVia      { get; set; }
        public decimal   Freight      { get; set; }
        public DateTime? OrderDate    { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate  { get; set; }

		public IEnumerable<Deal> Deals { get; set; }

        public Employee  Employee { get; set; }
        public Customer  Customer { get; set; }

        [JsonIgnore]
        public string CustomerName => Customer.CompanyName;

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Code => OrderID.ToString();
        [JsonIgnore]
        public string Lookup => OrderID.ToString();
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{OrderID} {CustomerID} {Customer?.CompanyName} {Employee?.LastName}";
        }
    }
}


