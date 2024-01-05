using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Pattern;
using Clio.Demo.Domain.Data.EqDeriv;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataManagement.Processor.EqD.DataModel
{
    public interface IAccountData : IDataAccess<Account>
    {
    }

    public sealed class AccountData : DataAccessMaster<Account>, IAccountData
    {
        public AccountData(ISqlGateway sqlGateway, IConfiguration configuration) : base(sqlGateway, configuration)
        {
        }
    }
}
