using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Pattern;
using Clio.Demo.Domain.Data.EqDeriv;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.EqD.DataModel
{
    public interface IOpenPositionData : IDataAccess<OpenPosition>
    {
    }

    public sealed class OpenPositionData : DataAccessMaster<OpenPosition>, IOpenPositionData
    {
        public OpenPositionData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
