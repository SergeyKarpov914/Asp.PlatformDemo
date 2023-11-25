using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface IOrderData : Abstraction.Interface.Mk2.IDataAccess<Order>
    {
    }

    public sealed class OrderData : DataAccessMaster<Order>, IOrderData
    {
        public OrderData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
