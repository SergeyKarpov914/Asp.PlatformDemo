using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.DataModel
{
    public interface ICustomerDataAccess : IDataAccess<Customer>
    {
    }

    public sealed class CustomerDataAccess : DataAccessMaster<Customer>, ICustomerDataAccess
    {
        public CustomerDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
