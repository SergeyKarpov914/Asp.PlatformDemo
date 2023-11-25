using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.NW.DataModel
{
    public interface IOrderDataAccess : IDataAccess<Order>
    {
    }

    public sealed class OrderDataAccess : DataAccessMaster<Order>, IOrderDataAccess
    {
        public OrderDataAccess(ISqlGateway sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
