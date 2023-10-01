﻿using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.DataModel
{
    public interface IEmployeeDataAccess : IDataAccess<Employee>
    {
    }

    public sealed class EmployeeDataAccess : DataAccessMaster<Employee>, IEmployeeDataAccess
    {
        public EmployeeDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
        {
        }
    }
}
