using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.Core.Component.Master.App;
using Clio.Demo.Core.Component.Master.Asp;
using Clio.Demo.Domain.Data.Processor;
using Clio.Demo.Domain.Data.Processor.DataModel;

namespace Clio.Demo.OrderCrudServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            new DemoWebAPI(Protocol.HttpSignalR).Run(args);
        }

        internal class DemoWebAPI : WebAPIAppMaster
        {
            public DemoWebAPI(Protocol protocol) : base(protocol) { }

            protected override void addCustomInjectables()
            {
                _services.AddTransient<ISqlGateway, SqlDapperGateway>();

                _services.AddTransient<IOrderData, OrderData>();
                _services.AddTransient<ICustomerData, CustomerData>();
                _services.AddTransient<IEmployeeData, EmployeeData>();
                _services.AddTransient<IProductData, ProductData>();

                _services.AddTransient<OrderCrudProcessor>();
            }

            protected override Type[] assemblies()
            {
                return new Type[]
                {
                    typeof(WebAPIAppMaster),
                };
            }
        }
    }
}