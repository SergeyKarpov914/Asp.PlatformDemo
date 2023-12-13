using Clio.Demo.Core.Component.Master;
using Clio.Demo.DataPresentation.Gateway;
using Clio.Demo.DataPresentation.ViewModel;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace Clio.Demo.BlazerServerAppDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new BlazorDataServer().Run(args);      // Middleware pipeline logic is encapsulated in a Master class
        }
    }

    public class BlazorDataServer : WebBlazorAppMaster
    {
        protected override void addCustomInjectables()
        {
            _services.AddSingleton<NorthwindGateway>();   // gateway to WebAPI server, injected into ViewModel
            _services.AddSingleton<NorthwindViewModel>(); // ViewModel is to be available for injection into razor components
            
            _services.AddSyncfusionBlazor();
            // In non-demo solutions, the key is to be stored with other secrets
            SyncfusionLicenseProvider.RegisterLicense(getLocal("key"));
        }
    }
}