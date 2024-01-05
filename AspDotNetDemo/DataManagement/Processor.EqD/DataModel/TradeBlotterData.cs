using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Pattern;
using Clio.Demo.Domain.Data.EqDeriv;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.EqD.DataModel
{
    public interface ITradeBlotterData : IDataAccess<TradeBlotter>
    {
    }

    public sealed class TradeBlotterData : DataAccessMaster<TradeBlotter>, ITradeBlotterData
    {
        public TradeBlotterData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
