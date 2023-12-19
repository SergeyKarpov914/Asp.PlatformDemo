using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Extension;
using Clio.Demo.Core.Util;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.NLog;
using Microsoft.Extensions.Configuration;


namespace Clio.Demo.Core
{
    public class DataAccessMaster<T> : IDataAccess<T> where T : class, IEntity, new()
    {
        #region c-tor

        private readonly string _typeName;

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

            _typeName = typeof(T).DisplayName();
        }

        #endregion c-tor

        public async Task Create(T entity)
        {
            Steps steps = Steps.Start(this, _typeName);
            try
            {
                await create(entity);
            }
            catch (Exception ex)
            {
                steps.Error(ex);
                throw;
            }
            finally
            {
                steps.Stop();
            }
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

        public async Task Update(T entity)
        {
            throw new NotImplementedException();
        }
        
        public async Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        #region visiting professor

        protected virtual async Task create<T>(T entity) where T : class, IEntity, new()
        {
            await _sqlClient.Insert<T>(entity, _connection);
        }

        protected virtual async Task<IEnumerable<T>> read(string clause = null)
        {
            return await _sqlClient.Read<T>(typeof(T).SelectQuery(clause), _connection);
        }

        #endregion visiting professor
    }
}
