using Clio.Demo.Abstraction.Interface;
using Clio.Demo.ConsoleDataManagement.Service;
using Clio.Demo.Core.Component.Master.App;
using Clio.Demo.Core.Gateway;
using Clio.Demo.DataManagement.Processor.NW.DataModel;
using Clio.Demo.DataManager.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace Clio.Demo.ConsoleDataManagement
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new ConsoleDataManagementHost().RunHosted(args);
        }
    }

    internal class ConsoleDataManagementHost : AppHostMaster
    {
        protected override void addServices(IServiceCollection services)
        {
            base.addServices(services);

            services.AddHttpClient();

            //services.AddApplicationInsightsTelemetry();     // if config contains "APPINSIGHTS_INSTRUMENTATIONKEY": "{instrumentation key}"

            services.AddTransient<ISqlGateway, SqlAdoGateway>();

            services.AddScoped<IOrderDataAccess    , OrderDataAccess>();
            services.AddScoped<ICustomerDataAccess , CustomerDataAccess>();
            services.AddScoped<IEmployeeDataAccess , EmployeeDataAccess>();
            services.AddScoped<IProductDataAccess  , ProductDataAccess>();
            services.AddScoped<ISupplierDataAccess , SupplierDataAccess>();
			services.AddScoped<IDealDataAccess     , DealDataAccess>();
			services.AddScoped<ITerritoryDataAccess, TerritoryDataAccess>();

            services.AddScoped<NorthwindProcessor>();
            services.AddHostedService<CrudTestService>();
        }
    }
}