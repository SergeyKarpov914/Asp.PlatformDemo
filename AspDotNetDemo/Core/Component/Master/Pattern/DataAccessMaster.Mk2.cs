using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Extension;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.NLog;
using Microsoft.Extensions.Configuration;


namespace Clio.Demo.Core
{
    public class DataAccessMaster<T> : Abstraction.Interface.Mk2.IDataAccess<T> where T : class, IEntity, new()
    {
        #region c-tor

        private string ConnectionConfig => $"Sql";
        private string configError(string config) => $"Cannot find '{config}' config entry";

        protected readonly IConfiguration _configuration;
        protected readonly ISqlGateway _sqlClient;
        
        private readonly string _connection;

        public DataAccessMaster(ISqlGateway sqlClient, IConfiguration configuration)
        {
            sqlClient    .Inject(out _sqlClient);
            configuration.Inject(out _configuration);

            _connection = configuration.GetConnectionString(ConnectionConfig) ?? throw new Exception(configError(ConnectionConfig));
        }

        #endregion c-tor

        public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Read(string clause = null)
        {
            IEnumerable<T> entities = null;
            try
            {
                entities = await read(clause);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            return entities;
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
        
        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        #region visiting professor

        protected virtual async Task<IEnumerable<T>> read(string clause = null)
        {
            return await _sqlClient.Read<T>(typeof(T).SelectQuery(clause), _connection);
        }

        #endregion visiting professor
    }
}
