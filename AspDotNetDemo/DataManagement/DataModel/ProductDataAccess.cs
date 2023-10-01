using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.DataModel
{
    public interface IProductDataAccess : IDataAccess<Product>
    {
    }

    public sealed class ProductDataAccess : DataAccessMaster<Product>, IProductDataAccess
    {
        public ProductDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
