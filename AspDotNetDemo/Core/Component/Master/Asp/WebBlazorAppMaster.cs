using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.Core.Component.Master.Asp;
using Clio.Demo.Core.Util;
using Clio.Demo.Extension;
using Microsoft.AspNetCore.Builder;

namespace Clio.Demo.Core.Component.Master
{
    public abstract class WebBlazorAppMaster : AspMaster
    {
        public async Task Run(string[] args)
        {
            WebApplicationBuilder builder = createWebAppBuilder(args);

            addTelemetry(builder);
            addContainerComponents(builder);

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

            if(segments.Length < 2)
            {
                throw new Exception($"Cannot get local {name}");
            }
            string local = segments[1].Replace("\"", "");
            return local;
        }
    }
}
