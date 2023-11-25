using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Util;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;
using Log = Clio.Demo.Util.Telemetry.Seri.Log;

namespace Clio.Demo.Core.Component.Master.App
{
    public abstract class WebAPIAppMaster
    {
        private IServiceCollection _services;

        private const string AllowOrigins = "AllowOrigins";

        protected IConfiguration _configuration;

        public void Run(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _configuration = builder.Configuration;

            #region set webhost

            int port = builder.Configuration.GetValue<int>("port");
            string urls = $"http://localhost:{port};http://*:{port + 1}";

            builder.WebHost.UseUrls(urls);

            #endregion set webhost

            #region Insights, Telemetry

            builder.Services.AddLogging();
            builder.Logging.AddJsonConsole();

            builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.AddScoped<ILog, LogMaster>();

            TelemetryDebugWriter.IsTracingDisabled = true;

            builder.Host.UseSerilog(); // serilog extension 

            Log.Initialize(builder.Services.BuildServiceProvider().GetRequiredService<ILog>());

            #endregion

            builder.Services.AddSignalR();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigins, policy => { policy.AllowAnyOrigin()
                                                                        .AllowAnyHeader()
                                                                        .AllowAnyMethod(); });
            });
            builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

            /////////////////////////////////////////////////////////////////////////
            addCustomInjectables(builder.Services);
            /////////////////////////////////////////////////////////////////////////

            WebApplication app = builder.Build();

            app.UseCors(AllowOrigins);
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            /////////////////////////////////////////////////////////////////////////
            addSignalRHub(app);
            /////////////////////////////////////////////////////////////////////////
            
            LogUtil.RuntimeInfo(this, $"WebApiApp server start", args, assemblies());

            Asp.ProcessConfiguration(this, args);
            Asp.ProcessDIContainer(this, _services, _configuration);

            Log.Block(this, new[] { $"API Server is listening on {urls.Replace("*", Dns.GetHostEntry(Dns.GetHostName()).HostName)}" }, "WebAPI Hosting start");

            app.Run();
        }

        protected virtual void addCustomInjectables(IServiceCollection services)
        {
            _services = services;
        }
        protected virtual void addSignalRHub(WebApplication app)
        {
        }

        protected abstract Type[] assemblies();
    }
}
