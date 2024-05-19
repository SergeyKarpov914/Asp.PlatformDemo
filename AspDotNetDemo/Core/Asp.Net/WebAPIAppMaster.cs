using Clio.Demo.Core.Lib.Util;
using Clio.Demo.Core7.Component;
using Microsoft.AspNetCore.Builder;
using System.Net;

namespace Clio.Demo.Core7.Asp
{
    public abstract class WebAPIAppMaster : AspNetCore
    {
        public WebAPIAppMaster(Protocol protocol = Protocol.Http) : base(protocol) { }

        public void Run(string[] args)
        {
            WebApplicationBuilder builder = createWebAppBuilder(args);

            addWebHost(builder);
            addTelemetry(builder);
            addContainerComponents(builder);
            
            addCustomInjectables(); // by implementation class

            WebApplication app = createWebApp(builder);

            //addSignalRHub(app);

            LogUtil.RuntimeInfo(this, $"WebApiApp server start", args, assemblies());

            processConfiguration(args);
            processDIContainer(false);

            Log.Block(this, new[] { $"API Server is listening on {_urls.Replace("*", Dns.GetHostEntry(Dns.GetHostName()).HostName)}" }, "WebAPI Hosting start");

            app.Run();
        }

        protected abstract Type[] assemblies();
    }
}
