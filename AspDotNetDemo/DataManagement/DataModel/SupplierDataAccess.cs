using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.DataModel
{
    public interface ISupplierDataAccess : IDataAccess<Supplier>
    {
    }

    public sealed class SupplierDataAccess : DataAccessMaster<Supplier>, ISupplierDataAccess
    {
        public SupplierDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
