using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.Core.Component.Master;
using Clio.Demo.DataManagement.Processor.EqD;
using Clio.Demo.DataManagement.Processor.EqD.DataModel;
using Clio.Demo.DataPresentation.ViewModel;
using Radzen;

namespace BlazorSalesDashboard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new BlazorSalesDashboardServer().Run(args);      // Middleware pipeline logic is encapsulated in a Master class
        }
    }

    public sealed class BlazorSalesDashboardServer : WebBlazorAppMaster
    {
        protected override void addCustomInjectables()
        {
            _services.AddHttpClient();
            _services.AddTransient<ISqlGateway, SqlDapperGateway>();

            _services.AddScoped<IAccountData,      AccountData>();
            _services.AddScoped<IOpenPositionData, OpenPositionData>();
            _services.AddScoped<ITradeBlotterData, TradeBlotterData>();

            _services.AddScoped<EqDerivProcessor>();
            _services.AddScoped<EqDerivViewModel>(); // ViewModel is to be available for injection into razor components
            
            _services.AddRadzenComponents();
        }
    }
}
