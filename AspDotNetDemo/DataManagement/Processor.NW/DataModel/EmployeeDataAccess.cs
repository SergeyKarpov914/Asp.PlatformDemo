using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Pattern;
using Clio.Demo.Domain.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.NW.DataModel
{
    public interface IEmployeeDataAccess : IDataAccess<Employee>
    {
    }

    public sealed class EmployeeDataAccess : DataAccessMaster<Employee>, IEmployeeDataAccess
    {
        public EmployeeDataAccess(ISqlGateway sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
