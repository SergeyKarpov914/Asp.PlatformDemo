using Clio.Demo.Core7.Pattern;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clio.Demo.Domain.Data.Northwind
{
    [Table("[Northwind].[dbo].[Orders]")]
    public class Order : EntityMaster
    {
        [Column("OrderID")]      public int       OrderID      { get; set; }
        [Column("EmployeeID")]   public int       EmployeeID   { get; set; }
        [Column("CustomerID")]   public int       CustomerID   { get; set; }
        [Column("ShipVia")]      public int       ShipVia      { get; set; }
        [Column("Freight")]      public decimal   Freight      { get; set; }
        [Column("OrderDate")]    public DateTime? OrderDate    { get; set; }
        [Column("RequiredDate")] public DateTime? RequiredDate { get; set; }
        [Column("ShippedDate")]  public DateTime? ShippedDate  { get; set; }

        [JsonIgnore] public IEnumerable<Deal> Deals    { get; set; }
        [JsonIgnore] public Employee          Employee { get; set; }
        [JsonIgnore] public Customer          Customer { get; set; }
    }
}


