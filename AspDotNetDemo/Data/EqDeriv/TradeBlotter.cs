using Clio.Demo.Core.Component.Master.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.EqDeriv
{
    [Table("[EqDeriv].[dbo].[TradeBlotter]")]
    public sealed class TradeBlotter : EntityMaster
    {
        [Column("TRADEDATE")]   public int    TradeDate  { get; set; }
        [Column("CLIENTID")]    public string ClientId   { get; set; }
        [Column("CLIENT")]      public string Client     { get; set; }
        [Column("BUYSELL")]     public string BuySell    { get; set; }
        [Column("QUANTITY")]    public double Quantity   { get; set; }
        [Column("UNDERLYING")]  public string Underlying { get; set; }
        [Column("EXPIRATION")]  public int    Expiration { get; set; }
        [Column("STRIKE")]      public double Strike     { get; set; }
        [Column("PUTCALL")]     public string PutCall    { get; set; }
        [Column("PRICE")]       public double Price      { get; set; }
        [Column("OTC")]         public bool   Otc        { get; set; }
    }
}

