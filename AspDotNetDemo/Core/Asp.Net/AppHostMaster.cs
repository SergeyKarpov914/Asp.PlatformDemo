using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Pattern;
using Clio.Demo.Core.Lib.Util;
using Clio.Demo.Core7.Component;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Clio.Demo.Core7.Asp
{
    public abstract class AppHostMaster : AspNetCore
    {
        public async Task RunHosted(string[] args)
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder(args)
                                 .ConfigureServices(addServices)
                                 .Build();
#if SERILOG
                Log.Initialize(host.Services.GetRequiredService<ILog>());
#endif
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
