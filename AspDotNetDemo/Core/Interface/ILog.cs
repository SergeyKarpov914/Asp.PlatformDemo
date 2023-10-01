using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Core.Interface
{
    public interface ILog
    {
        IConfiguration Configuration { get; }
        TelemetryClient TelemetryClient { get; }
    }
}
