using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Hbc;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface ICustomerData : Abstraction.Interface.Mk2.IDataAccess<Customer>
    {
    }

    public sealed class CustomerData : DataAccessMaster<Customer>, ICustomerData
    {
        public CustomerData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
