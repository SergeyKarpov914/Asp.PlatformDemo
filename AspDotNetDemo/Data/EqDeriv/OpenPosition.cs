using Clio.Demo.Core7.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.EqDeriv
{
    [Table("[EqDeriv].[dbo].[OpenPosition]")]
    public sealed class OpenPosition : EntityMaster

    {
        [Column("MASTERCODE")]         public string   MasterCode         { get; set; }
        [Column("TRADEDATE")]          public int      TradeDate          { get; set; }
        [Column("SYMBOL")]             public string   Symbol             { get; set; }
        [Column("QUANTITY")]           public int      Quantity           { get; set; }
        [Column("EXPIRATION")]         public int      Expiration         { get; set; }
        [Column("STRIKE")]             public double   Strike             { get; set; }
        [Column("PUTCALL")]            public string   PutCall            { get; set; }
        [Column("PRICE")]              public double   Price              { get; set; }
        [Column("CURRENTPRICE")]       public double   CurrentPrice       { get; set; }
        [Column("DELTA")]              public double   Delta              { get; set; }
        [Column("LASTUPDATED")]        public DateTime LastUpdated        { get; set; }
        [Column("CREATEDATE")]         public int      CreateDate         { get; set; }
        [Column("LASTTRADEDATE")]      public int      LastTradeDate      { get; set; }
        [Column("LASTPOSITIONEFFECT")] public string   LastPositionEffect { get; set; }
        [Column("CONSISTENCY")]        public string   Comsistency        { get; set; }
    }
}

