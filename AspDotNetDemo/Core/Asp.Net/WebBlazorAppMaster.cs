using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Core.Lib.Gateway;
using Clio.Demo.Core.Lib.Util;
using Clio.Demo.Core7.Component;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Clio.Demo.Core7.Asp
{
    public abstract class WebBlazorAppMaster : AspNetCore
    {
        public async Task Run(string[] args)
        {
            WebApplicationBuilder builder = createWebAppBuilder(args);

            addTelemetry(builder);
            addContainerComponents(builder);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            /////////////////////////////////////////////////////////////////////////
            addCustomInjectables();
            /////////////////////////////////////////////////////////////////////////

            WebApplication app = createBlazorApp(builder);

            LogUtil.RuntimeInfo(app.GetType(), "Starting", args, new[] { typeof(IEntity), typeof(SqlDapperGateway) });

            app.Run();
        }

        protected string getLocal(string name)
        {
            IEnumerable<string> lines = File.ReadLines("Data.txt");

            if (lines.IsEmpty())
            {
                throw new Exception($"Cannot get local {name}");
            }
            string[] segments = lines.First().Split('=');

            if (segments.Length < 2)
            {
                throw new Exception($"Cannot get local {name}");
            }
            string local = segments[1].Replace("\"", "");
            return local;
        }
    }
}
