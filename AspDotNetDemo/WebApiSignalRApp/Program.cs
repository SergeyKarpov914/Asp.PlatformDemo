using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.Core.Util;
using Clio.Demo.Domain.Data.Processor;
using Clio.Demo.Domain.Data.Processor.DataModel;
using Clio.Demo.Util.Telemetry.NLog;
using System.Net;

namespace Clio.Demo.OrderCrudServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            #region set webhost

            int port = builder.Configuration.GetValue<int>("port");
            string urls = $"https://localhost:{port};https://*:{port + 1}";

            builder.WebHost.UseUrls(urls);

            #endregion set webhost

            builder.Services.AddSignalR();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddLogging();
            builder.Logging.AddJsonConsole();

            builder.Services.AddSwaggerGen();

            #region Add custom injectables

            builder.Services.AddTransient<ISqlGateway, SqlDapperGateway>();

            builder.Services.AddTransient<IOrderData,    OrderData>();
            builder.Services.AddTransient<ICustomerData, CustomerData>();
            builder.Services.AddTransient<IEmployeeData, EmployeeData>();
            builder.Services.AddTransient<IProductData,  ProductData>();

            builder.Services.AddTransient<OrderCrudProcessor>();

            #endregion Add custom injectables

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(x => x.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .SetIsOriginAllowed(origin => true) 
                                  .AllowCredentials()); 
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapHub<OrderCrudHub>("/hub");

            LogUtil.RuntimeInfo(app.GetType(), "Starting", args, new[] { typeof(IEntity), typeof(SqlDapperGateway) });

            Log.Block(app.GetType(), new[] { $"API Server is listening on {urls.Replace("*", Dns.GetHostEntry(Dns.GetHostName()).HostName)}" }, "WebAPI Hosting start");

            app.Run();
        }
    }
}