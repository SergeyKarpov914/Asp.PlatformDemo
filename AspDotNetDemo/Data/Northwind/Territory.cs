using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Domain.Data.Northwind;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderService.Data
{
    [Table("[Northwind].[dbo].[Territories]")]
    public class Territory : EntityMaster
    {
        [Column("EmployeeID")]           public int    EmployeeID           { get; set; }
        [Column("TerritoryID")]          public string TerritoryID			{ get; set; }
        [Column("TerritoryDescription")] public string TerritoryDescription	{ get; set; }
        [Column("RegionDescription")]    public string RegionDescription	{ get; set; }

        [JsonIgnore]  public Employee Employee { get; set; }
    }
}
