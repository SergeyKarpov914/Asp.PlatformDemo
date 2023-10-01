using Clio.Demo.Abstraction.Data;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstractions.Interface;
using Clio.Demo.Core.Util;
using Clio.Demo.Extension;
using Clio.Demo.Util;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Core.Component.Master.Pattern
{
    #region data key implementation

    public class DataKey<D> : Key<D, KeyType>
    {
        public static DataKey<int> Id(int value, string column = null) { return new DataKey<int> { Value = value, Column = column, ValueType = KeyType.EntityId }; }
        public static DataKey<string> Code(string value, string column = null) { return new DataKey<string> { Value = value, Column = column, ValueType = KeyType.EntityCode }; }

        public override string SqlValue => Sql.SqlFormat(Value);
    }

    public class DataMultiKey<D> : MultiKey<D, KeyType> 
    {
        public static DataMultiKey<int>    Id  (IEnumerable<int> values, string column = null) { return new DataMultiKey<int> { Values = values, Column = column, ValueType = KeyType.EntityId }; }
        public static DataMultiKey<string> Code(IEnumerable<string> values, string column = null) { return new DataMultiKey<string> { Values = values, Column = column, ValueType = KeyType.EntityCode }; }

        public override IEnumerable<string> SqlValues => Values?.Select(x => Sql.SqlFormat(x)); 
    }

    #endregion data key implementation

    public class DataAccessMaster<T> : IDataAccess<T> where T : class, IEntity, new()
    {
        #region string literals

        private readonly string _type;

        private string ConnectionConfig => $"Sql";
        private string TableConfig => $"{_type}.Table";

        private string configError(string config) => $"Cannot find '{config}' config entry";

        #endregion string literals

        #region c-tor

        protected readonly ISQLClient _sqlClient;
        protected readonly IConfiguration _configuration;

        private readonly string _connection;
        protected readonly string _table;

        private IEnumerable<string> _columns;
        private IEnumerable<string> _columnsInsert;

        private IEnumerable<string> Columns => _columns ?? (_columns = _sqlClient.Columns(_table, _connection));
        private IEnumerable<string> ColumnsInsert => _columnsInsert ?? (_columnsInsert = _sqlClient.Columns(_table, _connection, ColumnSet.Insert));


        public DataAccessMaster(ISQLClient sqlClient, IConfiguration configuration)
        {
            sqlClient.Inject(out _sqlClient);
            configuration.Inject(out _configuration);

            _type = typeof(T).DisplayName();

            _connection = configuration.GetConnectionString(ConnectionConfig) ?? throw new Exception(configError(ConnectionConfig));
            _table = configuration.GetValue<string>(TableConfig) ?? throw new Exception(configError(TableConfig));
        }

        #endregion

        #region IDataAccess

        public async Task Create(T entity)
        {
            Steps steps = Steps.Start(this, $"Create {_type}");
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

        public async Task<T> Read(Key key)
        {
            Steps steps = Steps.Start(this, _type);
            T entity;
            try
            {
                entity = await read(key);
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
            return entity;
        }

        public async Task<IEnumerable<T>> ReadAll(MultiKey multiKey = null)
        {
            Steps steps = Steps.Start(this, _type);
            IEnumerable<T> entities = null;
            try
            {
    			entities = await readAll(multiKey);
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
            return entities;
        }

        public async Task Update(T entity)
        {
            Steps steps = Steps.Start(this, _type);
            try
            {
                await update(entity);
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

        public async Task Delete(T entity)
        {
            Steps steps = Steps.Start(this, _type);
            try
            {
                await delete(entity);
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

        public async Task Delete(MultiKey multiKey = null)
        {
            Steps steps = Steps.Start(this, _type);
            try
            {
                await delete(multiKey);
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

        public async Task<int> Count(Key key)
        {
            int rowCount = 0;
            Steps steps = Steps.Start(this, _type);
            try
            {
                rowCount = await count();
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
            return rowCount;
        }

        public bool IsTest { get; set; }

        #endregion IDataProvider

        #region visiting professor

        protected virtual async Task create(T entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task<T> read(Key key)
        {
            string query = Sql.SelectQuery(Columns, _table, Sql.SqlClause(key.Column, key.SqlValue));

            IEnumerable<T> items = await _sqlClient.Read<T>(query, _connection);

            return items.FirstOrDefault() as T;
        }

        protected virtual async Task<IEnumerable<T>> readAll(MultiKey multiKey = null)
        {
            string query = Sql.SelectQuery(Columns, _table, multiKey is null ? null : Sql.SqlClause(multiKey.Column, multiKey?.SqlValues));
            
            IEnumerable<T> items = await _sqlClient.Read<T>(query, _connection);

            return items;
        }

        protected virtual async Task update(T entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task delete(T entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task delete(MultiKey multiKey)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task<int> count(Key key = null)
        {
            return await Task.FromResult(default(int));
        }

        protected virtual async Task<bool> exists(T entity)
        {
            return await Task.FromResult(false);
        }

        #endregion visiting professor
    }
}
