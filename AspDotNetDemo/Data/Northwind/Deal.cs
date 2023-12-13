using Clio.Demo.Core.Component.Master.Pattern;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clio.Demo.Domain.Data.Northwind
{
    [Table("[Northwind].[dbo].[Deal]")]
    public class Deal : EntityMaster
	{
        [Column("OrderID")]   public int     OrderID   { get; set; }
        [Column("ProductID")] public int     ProductID { get; set; }
        [Column("Quantity")]  public int     Quantity  { get; set; }
        [Column("UnitPrice")] public decimal UnitPrice { get; set; }
        [Column("Discount")]  public decimal Discount  { get; set; }

        [JsonIgnore] public Product Product { get; set; }
	}
}
