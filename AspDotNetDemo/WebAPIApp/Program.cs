using Clio.Demo.Core.Component.Master.App;
using Clio.Demo.Core.Gateway;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Util;
using Clio.Demo.DataManager.Processor;
using Microsoft.Extensions.DependencyInjection;
using Clio.Demo.DataManagement.Processor.NW.DataModel;

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
            protected override void addCustomInjectables(IServiceCollection services)
            {
                base.addCustomInjectables(services);

                services.AddTransient<ISqlGateway, SqlAdoGateway>();

                services.AddTransient<IOrderDataAccess    , OrderDataAccess    >();
                services.AddTransient<ICustomerDataAccess , CustomerDataAccess >();
                services.AddTransient<IEmployeeDataAccess , EmployeeDataAccess >();
                services.AddTransient<IProductDataAccess  , ProductDataAccess  >();
                services.AddTransient<ISupplierDataAccess , SupplierDataAccess >();
				services.AddTransient<IDealDataAccess     , DealDataAccess>();
				services.AddTransient<ITerritoryDataAccess, TerritoryDataAccess>();

                services.AddTransient<NorthwindProcessor>();
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