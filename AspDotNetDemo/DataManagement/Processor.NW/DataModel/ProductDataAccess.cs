using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.NW.DataModel
{
    public interface IProductDataAccess : IDataAccess<Product>
    {
    }

    public sealed class ProductDataAccess : DataAccessMaster<Product>, IProductDataAccess
    {
        public ProductDataAccess(ISqlGateway sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
