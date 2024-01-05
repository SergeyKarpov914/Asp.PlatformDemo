using Clio.Demo.Core7.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Hbc
{
    [Table("[Hbc].[dbo].[Customer]")]
    public sealed class Product : EntityMaster
    {
        [Column("Id")]    public int     Id    { get; set; }
        [Column("Name")]  public string  Name  { get; set; }
        [Column("Price")] public decimal Price { get; set; }
    }
}
