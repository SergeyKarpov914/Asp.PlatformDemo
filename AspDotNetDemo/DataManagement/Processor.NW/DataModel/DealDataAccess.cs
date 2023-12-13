using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.NW.DataModel
{
    public interface IDealDataAccess : IDataAccess<Deal>
    {
    }

    public sealed class DealDataAccess : DataAccessMaster<Deal>, IDealDataAccess
    {
        public DealDataAccess(ISqlGateway sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
