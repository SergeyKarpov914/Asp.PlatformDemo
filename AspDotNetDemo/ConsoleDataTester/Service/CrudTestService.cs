using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Data.Northwind;
using Clio.Demo.DataManager.Processor;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Extensions.Configuration;
using Clio.Demo.Core.Component.Service;
using System.Diagnostics;

namespace Clio.Demo.ConsoleDataManagement.Service
{
    public sealed class CrudTestService : HostedService
    {
        public CrudTestService(NorthwindProcessor processor, IConfiguration configuration, IHttpClientFactory clientFactory) : base(configuration, clientFactory)
        {
            processor.Inject<NorthwindProcessor>(out _processor);
        }
        public CrudTestService(NorthwindProcessor processor, IConfiguration configuration) : base(configuration)
        {
            processor.Inject<NorthwindProcessor>(out _processor);
        }

        protected async override Task start(CancellationToken cancellationToken)
        {
            await testWebApi();
        }

        #region test inproc processor

        private readonly NorthwindProcessor _processor;

        private async Task testInprocProcessor()
        {
            Customer customer1 = await _processor.GetCustomer(2);
            Customer customer2 = await _processor.GetCustomer(4);

            IEnumerable<Customer> customers1 = await _processor.GetCustomers(new[] { 1, 4 });
            IEnumerable<Customer> customers2 = await _processor.GetCustomers(new[] { 4, 6 });

            Log.Info(this, $"{customer1}");
            Log.Info(this, $"{customer2}");
            Log.Info(this, $"{string.Join(", ", customers1.Select(x => x.ToString()))}");
            Log.Info(this, $"{string.Join(", ", customers2.Select(x => x.ToString()))}");
        }

        #endregion inproc processor

        #region test remote WebAPI via http

        private const string ApiName = "Northwind";

        private async Task testWebApi()
        {
            ApiHttpClient apiClient = new ApiHttpClient(ApiName, _clientFactory, _configuration);

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Testing {ApiName} WebAPI @{_configuration.GetValue<string>($"{ApiName}:Address")} Please enter entity name");
                string what = Console.ReadLine();

                Stopwatch timing = new Stopwatch();
                timing.Start();

                switch (what.ToLower())
                {
                    case "c":
                        IHttpResponse<Customer[]> customers = await apiClient.GetAll<Customer>("api/customers");
                        processResponse<Customer>(customers, timing);
                        break;
                    case "e":
                        IHttpResponse<Employee[]> employees = await apiClient.GetAll<Employee>("api/employees");
                        processResponse<Employee>(employees, timing);
                        break;
                    case "s":
                        IHttpResponse<Supplier[]> suppliers = await apiClient.GetAll<Supplier>("api/suppliers");
                        processResponse<Supplier>(suppliers, timing);
                        break;
                    case "p":
                        IHttpResponse<Product[]> products = await apiClient.GetAll<Product>("api/products");
                        processResponse<Product>(products, timing);
                        break;
                    case "o":
                        IHttpResponse<Order[]> orders = await apiClient.GetAll<Order>("api/orders");
                        processResponse<Order>(orders, timing);
                        break;
                    case "q":
                        stop(0, "That's all, folks");
                        return;
                    default:
                        Console.WriteLine("Who do you think you are? Nigel Mansell?");
                        Console.WriteLine("Valid inputs are: c (customers) e (employees) s (suppliers) p (products) o (orders) q (quit) ");
                        break;
                }
            }
        }

        private void processResponse<T>(IHttpResponse<T[]> response, Stopwatch timing)
        {
            timing.Stop();

            string time = $"({timing.Elapsed.TotalMilliseconds:N3} ms)";

            if (response.Code != 200)
            {
                Log.Info(this, $"{time} {response.Request}: {response.Code} {response.CodeName} {response.Message}");
            }
            else if (response.Data is not null)
            {
                Log.Block(this, (response.Data as T[]).Select(x => $"{x}"), $"{time} {response.Request}");
            }
        }

        #endregion remote WebAPI via http
    }
}