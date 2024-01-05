using Clio.Demo.Core.Util;
using Clio.Demo.Core7.Component;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

using Log = Clio.Demo.Util.Telemetry.Seri.Log;

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

            /////////////////////////////////////////////////////////////////////////
            addCustomInjectables();
            /////////////////////////////////////////////////////////////////////////

            WebApplication app = createWebApp(builder);

            /////////////////////////////////////////////////////////////////////////
            addSignalRHub(app);
            /////////////////////////////////////////////////////////////////////////

            processConfiguration(args);
            processDIContainer();

            LogUtil.RuntimeInfo(this, $"WebApiApp server start", args, assemblies());
            Log.Block(this, new[] { $"API Server is listening on {_urls.Replace("*", Dns.GetHostEntry(Dns.GetHostName()).HostName)}" }, "WebAPI Hosting start");

            app.Run();
        }

        protected abstract Type[] assemblies();
    }
}
