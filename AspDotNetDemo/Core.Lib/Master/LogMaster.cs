using Clio.Demo.Abstraction.Interface;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Core.Lib.Pattern
{
    public sealed class LogMaster : ILog
    {
        public IConfiguration Configuration { get; private set; }
        public TelemetryClient TelemetryClient { get; private set; }

        public LogMaster(IConfiguration configuration, TelemetryClient telemetryClient)
        {
            Configuration = configuration;
            TelemetryClient = telemetryClient;
        }
        public LogMaster(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
