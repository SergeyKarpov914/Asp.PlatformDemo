using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Core.Lib.Pattern;
using Clio.Demo.Core.Lib.Util;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

using Log = Clio.Demo.Core.Lib.Util.Log;

namespace Clio.Demo.Core7.Component
{
    public enum Target { Production, Demo, Staging, Development, Poc }
    public enum Hosting { CommandLine, Swagger }
    public enum Protocol { Http, HttpSignalR, SignalR }

    public abstract class AspNetCore
    {
        #region constants

        private const string AllowOrigins = "AllowOrigins";

        private const string ConfigName = "appsettings";
        private const string ConfigExt = "json";
        private const string ConfigEnv = "environment";
        private const string ShutdownEnv = "shutdown";

        public static Target TargetEnvironment { get; private set; }

        private const string Serilog = "SeriLog";
        private const string Nlog = "NLog";

        protected readonly Protocol _protocol;
        protected string _urls;

        #endregion constants

        public AspNetCore(Protocol protocol = Protocol.Http)
        {
            _protocol = protocol;
        }

        protected IServiceCollection _services;
        protected IConfiguration _configuration;

        protected WebApplicationBuilder createWebAppBuilder(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _configuration = builder.Configuration;
            _services = builder.Services;

            return builder;
        }

        protected ILog addTelemetry(WebApplicationBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Logging.AddJsonConsole();

            builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.AddScoped<ILog, LogMaster>();

            TelemetryDebugWriter.IsTracingDisabled = true;

            switch (_configuration.GetValue<string>("Logger"))
            {
                case Serilog:
                    builder.Host.UseSerilog(); // serilog extension 
                    break;
                case Nlog:
                default:
                    break;
            }
            return builder.Services.BuildServiceProvider().GetRequiredService<ILog>();
        }

        protected void addWebHost(WebApplicationBuilder builder)
        {
            int port = builder.Configuration.GetValue<int>("port");
            _urls = $"http://localhost:{port};http://*:{port + 1}";

            builder.WebHost.UseUrls(_urls);
        }

        protected void addContainerComponents(WebApplicationBuilder builder, Hosting hosting = Hosting.CommandLine)
        {
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigins, policy =>
                {
                    policy.AllowAnyOrigin()
                                                                       .AllowAnyHeader()
                                                                       .AllowAnyMethod();
                });
            });
            builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

            switch (hosting)
            {
                case Hosting.Swagger:
                    builder.Services.AddSwaggerGen();
                    break;
            }
        }

        protected WebApplication createWebApp(WebApplicationBuilder builder, Hosting hosting = Hosting.CommandLine)
        {
            WebApplication app = builder.Build();

            switch (hosting)
            {
                case Hosting.Swagger:
                    useSwagger(app);
                    break;
                default:
                    app.UseCors(AllowOrigins);
                    break;
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            switch (_protocol)
            {
                case Protocol.SignalR:
                case Protocol.HttpSignalR:
                    addSignalRHub(app);
                    break;
                default:
                    break;
            }
            return app;
        }

        protected void useSwagger(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(x => x.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .SetIsOriginAllowed(origin => true)
                                  .AllowCredentials());
            }
        }

        protected WebApplication createBlazorApp(WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();

            if (!app.Environment.IsDevelopment())  // Configure the HTTP request pipeline.
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();                     // The default HSTS value is 30 days, see https://aka.ms/aspnetcore-hsts.
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();

            app.MapFallbackToPage("/_Host");

            return app;
        }

        protected virtual void addCustomInjectables()
        {
        }
        protected virtual void addSignalRHub(WebApplication app)
        {
        }

        protected IConfigurationRoot processConfiguration(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                         .AddJsonFile(getAppConfigName(args), optional: false, reloadOnChange: true)
                                                                         .AddCommandLine(args)
                                                                         .Build();
            LogUtil.ConfigurationContent(this, configuration);

            return configuration;
        }

        private string getAppConfigName(string[] args)
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

        protected void processDIContainer()
        {
            string prefix = _configuration.GetValue<string>("prefix") ?? "n/a";

            var InjectionGroups = _services.GroupBy(s => s.ServiceType.Namespace)
                                          .Select(group => new
                                          {
                                              source = group.Key,
                                              count = group.Count()
                                          })
                                          .OrderBy(x => x.source);

            Log.Block(this, InjectionGroups.Where(x => !x.source.StartsWith(prefix)).Select(x => $"{x.count.ToString().PadLeft(6)} {x.source}"), "Injection Container ASP", true);

            if (prefix.IsNotEmpty())
            {
                Log.Block(this, _services.Where(x => x.ServiceType.Namespace.StartsWith(prefix))
                                            .Select(x => $"{x.ServiceType.DisplayName().PadLeft(20)} {x.Lifetime}"), $"Injection Container '{prefix}'", true);
            }
        }

    }
}
