using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Microsoft.Extensions.Configuration;
using OrderService.Data;

namespace Clio.Demo.DataManagement.DataModel
{
    public interface ITerritoryDataAccess : IDataAccess<Territory>
    {
    }

    public sealed class TerritoryDataAccess : DataAccessMaster<Territory>, ITerritoryDataAccess
    {
        public TerritoryDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
