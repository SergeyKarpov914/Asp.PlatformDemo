using Clio.Demo.Core7.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.EqDeriv
{
    [Table("[EqDeriv].[dbo].[Account]")]
    public sealed class Account : EntityMaster
    {
        [Column("MASTERCODE")]   public string MasterCode  {get;set;}
        [Column("DESKCODE")]     public string DeskCode    {get;set;}
        [Column("ACCOUNTNAME")]  public string AccountName {get;set;}
        [Column("STATUS")]       public string Status      {get;set;}
        [Column("LASTUPDATED")]  public string LastUpdated {get;set;}
    }
}
