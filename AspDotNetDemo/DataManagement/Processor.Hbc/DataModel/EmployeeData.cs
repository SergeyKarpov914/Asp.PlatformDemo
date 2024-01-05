using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Pattern;
using Clio.Demo.Domain.Data.Hbc;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Domain.Data.Processor.DataModel
{
    public interface IEmployeeData : IDataAccess<Employee>
    {
    }

    public sealed class EmployeeData : DataAccessMaster<Employee>, IEmployeeData
    {
        public EmployeeData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
