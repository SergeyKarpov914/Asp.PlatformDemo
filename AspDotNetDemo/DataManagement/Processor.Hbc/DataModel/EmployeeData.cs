using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core;
using Clio.Demo.Domain.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface IEmployeeData : Abstraction.Interface.Mk2.IDataAccess<Employee>
    {
    }

    public sealed class EmployeeData : DataAccessMaster<Employee>, IEmployeeData
    {
        public EmployeeData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
