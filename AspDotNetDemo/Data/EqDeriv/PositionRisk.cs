using Clio.Demo.Core.Component.Master.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.EqDeriv
{
    [Table("[EqDeriv].[dbo].[PositionRisk]")]
    public sealed class PositionRisk : EntityMaster
    {
        [Column("EODDATE")]     public int    EodDate     { get; set; }
        [Column("UNDERLYING")]  public string Underlying  { get; set; }
        [Column("DOLLARDELTA")] public double DollarDelta { get; set; }
        [Column("DOLLARGAMMA")] public double DollarGamma { get; set; }
        [Column("DOLLARVEGA")]  public double DollarVega  { get; set; }
        [Column("DOLLARTHETA")] public double DollarTheta { get; set; }
        [Column("DOLLARRHO")]   public double DollarRho   { get; set; }
        [Column("SHAREDELTA")]  public double ShareDelta  { get; set; }
        [Column("SHAREGAMMA")]  public double ShareGamma  { get; set; }
    }
}
