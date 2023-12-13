using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.NW.DataModel
{
    public interface ISupplierDataAccess : IDataAccess<Supplier>
    {
    }

    public sealed class SupplierDataAccess : DataAccessMaster<Supplier>, ISupplierDataAccess
    {
        public SupplierDataAccess(ISqlGateway sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
