using Clio.Demo.Core.Component.Master;
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
             _services.AddSingleton<EqDerivViewModel>(); // ViewModel is to be available for injection into razor components
             _services.AddRadzenComponents();
        }
    }
}
