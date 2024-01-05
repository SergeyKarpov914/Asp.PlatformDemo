using Clio.Demo.Core7.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Hbc
{
    [Table("[Hbc].[dbo].[Order]")]
    public sealed class Order : EntityMaster
    {
        [Column("Id")]            public int Id            { get; set; }
        [Column("SalesPersonId")] public int SalesPersonId { get; set; }
        [Column("CustomerId")]    public int CustomerId    { get; set; }
        [Column("ProductId")]     public int ProductId     { get; set; }
        [Column("Quantity")]      public int Quantity      { get; set; }
    }
}
