using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Util;
using Clio.Demo.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Log = Clio.Demo.Util.Telemetry.Seri.Log;

namespace Clio.Demo.Core.Component.Master.App
{
    public enum Target { Production, Demo, Staging, Development, Poc }

    public abstract class AppHostMaster
    {
        private   IServiceCollection _services;
        protected IConfigurationRoot _configuration;

        public async Task RunHosted(string[] args)
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder(args)
                                 .ConfigureServices(addServices)
                                 .Build();

                Log.Initialize(host.Services.GetRequiredService<ILog>());

                LogUtil.RuntimeInfo(GetType(), "Starting", args, new[] { GetType() });

                _configuration = Asp.ProcessConfiguration(this, args);
                Asp.ProcessDIContainer(this, _services, _configuration);

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

        #region config provider

        private const string ConfigName = "appsettings";
        private const string ConfigExt = "json";
        private const string ConfigEnv = "environment";
        private const string ShutdownEnv = "shutdown";

        public static Target TargetEnvironment { get; private set; }

        private void processConfiguration(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                         .AddJsonFile(getAppConfigName(args), optional: false, reloadOnChange: true)
                                                                         .AddCommandLine(args)
                                                                         .Build();
            LogUtil.ConfigurationContent(this, configuration);
            
            _configuration = configuration;
        }

        private void processDIContainer()
        {
            Log.BlockDbg(this, _services.Select(x => $"{x.ServiceType.DisplayName().PadLeft(60)} {x.Lifetime}"), "Injection Container");

            foreach(ServiceDescriptor sd in _services)
            {
                if (sd.ImplementationType is null && sd.ImplementationInstance is null && sd.ImplementationFactory is null)
                {
                    Log.Warn(this, $"{sd.ServiceType} has no implementation policy");
                }
            }
        }

        private static string getAppConfigName(string[] args)
        {
            string environment = string.Empty; // Cmd.GetSetting<string>(args, ConfigEnv, x => x.ToString());

            //if (!Enum.GetNames(typeof(Target)).Any(x => x == environment))
            //{
            //    throw new Exception($"Command line 'environment=' argument must be one of: '{string.Join(", ", Enum.GetNames(typeof(Target)))}'");
            //}
            //TargetEnvironment = (Target)Enum.Parse(typeof(Target), environment); // environment string is known to parse correctly after previous check

            return $"{ConfigName}.{ConfigExt}";
        }

        #endregion config provider
    }
}
