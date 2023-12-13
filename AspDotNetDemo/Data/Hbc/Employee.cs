using Clio.Demo.Core.Component.Master.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Hbc
{
    [Table("[Hbc].[dbo].[Employee]")]
    public sealed class Employee : EntityMaster
    {
        [Column("Id")]         public int    Id         { get; set; }
        [Column("FirstName")]  public string FirstName  { get; set; }
        [Column("MiddleName")] public string MiddleName { get; set; }
        [Column("LastName")]   public string LastName   { get; set; }
    }
}
