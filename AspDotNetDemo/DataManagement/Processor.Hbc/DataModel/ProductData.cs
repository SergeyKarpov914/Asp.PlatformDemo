using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface IProductData : Abstraction.Interface.Mk2.IDataAccess<Product>
    {
    }

    public sealed class ProductData : DataAccessMaster<Product>, IProductData
    {
        public ProductData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
