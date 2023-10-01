using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Util;
using Clio.Demo.Extension;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Log = Clio.Demo.Util.Telemetry.Seri.Log;

namespace Clio.Demo.Core.Component.Master
{
    public abstract class WebBlazorAppHost
    {
        protected IServiceCollection _services;
        protected IConfiguration _configuration;

        public async Task Run(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _configuration = builder.Configuration;

            #region Insights, Telemetry

            builder.Services.AddLogging();
            builder.Logging.AddJsonConsole();

            builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.AddScoped<ILog, LogMaster>();

            TelemetryDebugWriter.IsTracingDisabled = true;

            builder.Host.UseSerilog(); // serilog extension 

            Log.Initialize(builder.Services.BuildServiceProvider().GetRequiredService<ILog>());

            #endregion

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();

            // Add custom
            addAppServices(builder.Services);

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        protected abstract void addAppServices(IServiceCollection services);

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
