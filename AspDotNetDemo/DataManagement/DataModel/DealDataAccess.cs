using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Data.Northwind;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.DataModel
{
	public interface IDealDataAccess : IDataAccess<Deal>
	{
	}

	public sealed class DealDataAccess : DataAccessMaster<Deal>, IDealDataAccess
	{
		public DealDataAccess(ISQLClient sqlClient, IConfiguration configuration) : base(sqlClient, configuration)
		{
		}
	}
}
