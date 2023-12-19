using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Hbc;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface IOrderData : IDataAccess<Order>
    {
    }

    public sealed class OrderData : DataAccessMaster<Order>, IOrderData
    {
        public OrderData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
