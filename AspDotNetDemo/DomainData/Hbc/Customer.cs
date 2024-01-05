using Clio.Demo.Core.Lib.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Hbc
{
    [Table("[Hbc].[dbo].[Customer]")]
    public sealed class Customer : EntityMaster
    {
        [Column("Id")]         public int    Id         { get; set; }
        [Column("FirstName")]  public string FirstName  { get; set; }
        [Column("MiddleName")] public string MiddleName { get; set; }
        [Column("LastName")]   public string LastName   { get; set; }
    }
}
