using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Core.Gateway;
using Clio.Demo.Abstraction.Interface;
using Microsoft.Extensions.Configuration;
using Clio.Demo.Abstraction.Data;
using NUnit.Framework;
using static Clio.Demo.Util.Sql;
using Clio.Demo.Util;
using KellermanSoftware.CompareNetObjects;

namespace Clio.Demo.Core.Component.Master.Test
{
    public class DataAccessTestMaster<T> where T : class, IEntity, new()
    {
        protected virtual string AppSettings    => "tbd"; 
        protected virtual string ConnectionName => "tbd";
        protected virtual string TableName      => "tbd";

        protected ISQLClient     _sqlClient     = null;
        protected IConfiguration _configuration = null;
        protected IDataAccess<T> _dataAccess    = null;

        private CompareLogic compare = new CompareLogic();

        protected virtual Func<IDataAccess<T>>   DataAccessFactory => throw new Exception("Implementation test class must implement provider factory");
        protected virtual Func<BuilderMaster<T>> BuilderFactory    => throw new Exception("Implementation test class must implement provider factory");

        protected string Connection => _configuration.GetConnectionString(ConnectionName);

        protected CompareLogic Compare { get => compare; set => compare = value; }

        protected async Task setup()
        {
            try
            {
                _sqlClient     = new SQLClient();
                _configuration = new ConfigurationBuilder().AddJsonFile(AppSettings).Build();
                _dataAccess    = DataAccessFactory?.Invoke();
            }
            catch (Exception ex)
            {
            }
        }

        protected async Task assertTableDataIntegrity()
        {
            IEnumerable<IdGroupCount> rowCounts = await _sqlClient.Read<IdGroupCount>(Sql.GroupByQuery(), Connection);

            Assert.IsTrue(rowCounts.Count() > 0, $"Table {TableName} is expected to have rows");
            Assert.IsTrue(rowCounts.All(x => x.Count == 1), $"Table {TableName} is not expected to have rows with duplicate [{TestSet.EntityId.Column}] column values");
        }

        protected async Task<T> getBenchmarkEntity(Key key)
        {
            T entity = await runSqlForOne(key);
            Assert.IsNotNull(entity, "Entity from direct SQL expected not to be NULL");

            return entity;
        }

        protected void assertCreateDuplicate(T existing)
        {
            Assert.ThrowsAsync<DbUpdateException>(async () => await _dataAccess.Create(existing), $"_dataAccess.Create(entity) is expected to throw 'DbUpdateException' on attempt to create new entity with duplicate [{TestSet.EntityId.Column}] column values");
        }

        protected async Task assertCreate()
        {
            T newEntity = Builder.WithDefaults().Build();
            Assert.IsNotNull(newEntity, "Entity from EntityBuilder().WithDefaults().Build() expected not to be NULL");

            var count = await runSqlCount();

            await _dataAccess.Create(newEntity);

            var newCount = await runSqlCount();

            Assert.IsTrue(newCount == count + 1, $"After _dataAccess.Create(entity), {TableName} table count ({count}) is expected to be incremented by 1");

            //T testEntity = await runSqlForOne(Key.EntityCode(newEntity.Code));

            //Assert.IsTrue(testEntity.Id == newEntity.Code, "Entity built and entity in table from _dataAccess.Create(key) are expected to be equal");
        }

        protected async Task assertRead(Key key, Func<T, string> value)
        {
            T entity = await _dataAccess.Read(key.Value, key.ValueType);
            T testEntity = await getBenchmarkEntity(key);

            Assert.IsNotNull(entity, "Entity from _dataAccess.Read(key) expected not to be NULL");
            Assert.IsTrue(Compare.Compare(value(entity), key.Value).AreEqual, "Entity property and key value expected to be equal");
            Assert.IsTrue(Compare.Compare(entity, testEntity).AreEqual, "Entities from _dataAccess.Read(key) and from direct SQL expected to be equal");
        }

        protected async Task assertReadList(Key key)
        {
            List<T> entities = await _dataAccess.ReadList(key.Value, key.ValueType);
            List<T> testEntities = await runSqlForMulti(key);

            Assert.IsNotNull(entities, "List from _dataAccess.ReadList(key) expected not to be NULL");
            Assert.IsTrue(entities.Count == testEntities.Count, "List counts from _dataAccess.ReadList(key) and SQL query expected to be equal");

            List<T> sortedEf = entities.OrderBy(x => PrimaryKey(x)).ToList();
            List<T> sortedSql = testEntities.OrderBy(x => PrimaryKey(x)).ToList();

            for (var x = 0; x < sortedEf.Count; x++)
            {
                Assert.IsTrue(Compare.Compare(sortedEf[x], sortedSql[x]).AreEqual, "All entities from _dataAccess.ReadList(key) and from direct SQL expected to be equal");
            }
        }

    }
}
