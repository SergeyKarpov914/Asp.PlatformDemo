using Clio.Demo.Core.Component.Master.App;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clio.Demo.Core.Util
{
    public static class Asp
    {
        private const string ConfigName = "appsettings";
        private const string ConfigExt = "json";
        private const string ConfigEnv = "environment";
        private const string ShutdownEnv = "shutdown";

        public static Target TargetEnvironment { get; private set; }

        public static IConfigurationRoot ProcessConfiguration(object owner, string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                         .AddJsonFile(getAppConfigName(args), optional: false, reloadOnChange: true)
                                                                         .AddCommandLine(args)
                                                                         .Build();
            LogUtil.ConfigurationContent(owner, configuration);

            return configuration;
        }

        private static string getAppConfigName(string[] args)
        {
            string environment = string.Empty; // Cmd.GetSetting<string>(args, ConfigEnv, x => x.ToString());

            #region todo dev/prod env switch
            //if (!Enum.GetNames(typeof(Target)).Any(x => x == environment))
            //{
            //    throw new Exception($"Command line 'environment=' argument must be one of: '{string.Join(", ", Enum.GetNames(typeof(Target)))}'");
            //}
            //TargetEnvironment = (Target)Enum.Parse(typeof(Target), environment); // environment string is known to parse correctly after previous check
            #endregion todo dev/prod env switch

            return $"{ConfigName}.{ConfigExt}";
        }

        public static void ProcessDIContainer(object owner, IServiceCollection services, IConfiguration config)
        {
            string prefix = config.GetValue<string>("prefix") ?? "n/a";

            var InjectionGroups = services.GroupBy(s => s.ServiceType.Namespace)
                                          .Select(group => new { source = group.Key,
                                                                 count  = group.Count()})
                                          .OrderBy(x => x.source);

            Log.BlockDbg(owner, InjectionGroups.Where(x => !x.source.StartsWith(prefix)).Select(x => $"{x.count.ToString().PadLeft(6)} {x.source}"), "Injection Container ASP");

            if (prefix.IsNotEmpty())
            {
                Log.BlockDbg(owner, services.Where(x => x.ServiceType.Namespace.StartsWith(prefix))
                                            .Select(x => $"{x.ServiceType.DisplayName().PadLeft(20)} {x.Lifetime}"), $"Injection Container '{prefix}'");
            }
            #region not needed
            //foreach (ServiceDescriptor sd in services)
            //{
            //    if (sd.ImplementationType is null && sd.ImplementationInstance is null && sd.ImplementationFactory is null)
            //    {
            //        Log.Warn(owner, $"{sd.ServiceType} has no implementation policy");
            //    }
            //}
            #endregion not needed
        }
    }
}