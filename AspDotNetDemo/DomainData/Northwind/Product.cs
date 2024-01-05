using Clio.Demo.Core.Lib.Pattern;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clio.Demo.Domain.Data.Northwind
{
    [Table("[Northwind].[dbo].[Products]")]
    public class Product : EntityMaster
    {
        [Column("ProductID")]       public int     ProductID       { get; set; }
        [Column("SupplierID")]      public int     SupplierID      { get; set; }
        [Column("ProductName")]     public string  ProductName     { get; set; }
        [Column("OrderUnitPrice ")] public decimal OrderUnitPrice  { get; set; }
        [Column("Quantity")]        public short   Quantity        { get; set; }
        [Column("Discount")]        public float   Discount        { get; set; }
        [Column("QuantityPerUnit")] public string  QuantityPerUnit { get; set; }
        [Column("UnitPrice")]       public decimal UnitPrice       { get; set; }
        [Column("UnitsInStock")]    public short   UnitsInStock    { get; set; }
        [Column("UnitsOnOrder")]    public short   UnitsOnOrder    { get; set; }
        [Column("ReorderLevel")]    public short   ReorderLevel    { get; set; }
        [Column("Discontinued")]    public bool    Discontinued    { get; set; }

        [JsonIgnore] public Supplier Supplier { get; set; }
    }
}
