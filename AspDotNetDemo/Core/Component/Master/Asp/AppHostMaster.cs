using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Master.Asp;
using Clio.Demo.Core.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Log = Clio.Demo.Util.Telemetry.Seri.Log;

namespace Clio.Demo.Core.Component.Master.App
{
    public abstract class AppHostMaster : AspMaster
    {
        public async Task RunHosted(string[] args)
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder(args)
                                 .ConfigureServices(addServices)
                                 .Build();

                Log.Initialize(host.Services.GetRequiredService<ILog>());

                LogUtil.RuntimeInfo(GetType(), "Starting", args, new[] { GetType() });

                _configuration = processConfiguration(args);
                processDIContainer();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }

        protected virtual void addServices(IServiceCollection services)
        {
            services.AddScoped<ILog, LogMaster>();

            _services = services;
        }
    }
}
