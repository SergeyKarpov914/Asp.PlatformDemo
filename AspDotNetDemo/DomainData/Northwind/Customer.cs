using Clio.Demo.Core.Lib.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Northwind
{
    [Table("[Northwind].[dbo].[Customers]")]
    public sealed class Customer : EntityMaster
    {
        [Column("CustomerID")]   public int    CustomerID   { get; set; }
        [Column("CompanyName")]  public string CompanyName  { get; set; }
        [Column("ContactName")]  public string ContactName  { get; set; }
        [Column("ContactTitle")] public string ContactTitle { get; set; }
        [Column("Address")]      public string Address      { get; set; }
        [Column("City")]         public string City         { get; set; }
        [Column("Region")]       public string Region       { get; set; }
        [Column("PostalCode")]   public string PostalCode   { get; set; }
        [Column("Country")]      public string Country      { get; set; }
        [Column("Phone")]        public string Phone        { get; set; }
        [Column("Fax")]          public string Fax          { get; set; }
    }
}
