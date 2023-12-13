using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Master.App;
using Clio.Demo.Core.Gateway;
using Clio.Demo.DataManagement.Processor.NW.DataModel;
using Clio.Demo.DataManager.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPIApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new DemoWebAPI().Run(args);
        }

        internal class DemoWebAPI : WebAPIAppMaster
        {
            protected override void addCustomInjectables()
            {
                _services.AddTransient<ISqlGateway, SqlAdoGateway>();

                _services.AddTransient<IOrderDataAccess    , OrderDataAccess    >();
                _services.AddTransient<ICustomerDataAccess , CustomerDataAccess >();
                _services.AddTransient<IEmployeeDataAccess , EmployeeDataAccess >();
                _services.AddTransient<IProductDataAccess  , ProductDataAccess  >();
                _services.AddTransient<ISupplierDataAccess , SupplierDataAccess >();
				_services.AddTransient<IDealDataAccess     , DealDataAccess>();
				_services.AddTransient<ITerritoryDataAccess, TerritoryDataAccess>();

                _services.AddTransient<NorthwindProcessor>();
            }

            protected override Type[] assemblies()
            {
                return new Type[]
                {
                    typeof(WebAPIAppMaster),
                };
            }
        }
    }
}