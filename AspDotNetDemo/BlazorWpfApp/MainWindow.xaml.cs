using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Component.Gateway;
using Clio.Demo.DataManagement.Processor.EqD.DataModel;
using Clio.Demo.DataManagement.Processor.EqD;
using Clio.Demo.DataPresentation.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Radzen;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;

namespace BlazorWpfApp
{
    public partial class MainWindow : Window
    {
        IConfiguration _configuration;

        public MainWindow()
        {
            InitializeComponent();

            ServiceCollection serviceCollection = new ServiceCollection();

            _configuration = createConfiguration();

            serviceCollection.AddSingleton<IConfiguration> (_configuration);

            serviceCollection.AddWpfBlazorWebView();

            serviceCollection.AddHttpClient();
            serviceCollection.AddTransient<ISqlGateway, SqlDapperGateway>();

            serviceCollection.AddScoped<IAccountData, AccountData>();
            serviceCollection.AddScoped<IOpenPositionData, OpenPositionData>();
            serviceCollection.AddScoped<ITradeBlotterData, TradeBlotterData>();

            serviceCollection.AddScoped<EqDerivProcessor>();
            serviceCollection.AddScoped<EqDerivViewModel>(); // ViewModel is to be available for injection into razor components

            serviceCollection.AddRadzenComponents();

            Resources.Add("services", serviceCollection.BuildServiceProvider());
        }

        private IConfiguration createConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                             .Build();
        }
    }
}