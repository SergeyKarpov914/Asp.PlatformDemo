using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.EqDeriv;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.EqD.DataModel
{
    public interface IPositionRiskData : IDataAccess<PositionRisk>
    {
    }

    public sealed class PositionRiskData : DataAccessMaster<PositionRisk>, IPositionRiskData
    {
        public PositionRiskData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
