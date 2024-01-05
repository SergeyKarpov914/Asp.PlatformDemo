using Clio.Demo.Core.Lib.Util;
using Clio.Demo.Core7.Component;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace Clio.Demo.Core7.Asp.Net
{
    public abstract class HostedServiceMaster : ServiceCore, IHostedService
    {
        #region c-tor

        private AutoResetEvent Shutdown { get; set; } = new AutoResetEvent(false);

        public HostedServiceMaster(IConfiguration configuration) : base(configuration)
        {
        }
        public HostedServiceMaster(IConfiguration configuration, IHttpClientFactory clientFactory) : base(configuration, clientFactory)
        {
        }

        #endregion c-tor

        #region IHostedService

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Info(this, $"Starting hosted service...");

            int timeToShutdown = getShutdownTime();

            await start(cancellationToken);
            waitForShutdown(timeToShutdown);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            stop();
            await Task.CompletedTask;
        }

        #endregion IHostedService

        protected abstract Task start(CancellationToken cancellationToken);
        protected virtual void stop(int code = 0, string context = null)
        {
            LogUtil.ExitInfo(this, code, context);
            Environment.Exit(code);
        }

        private void waitForShutdown(int timeToShutdown)
        {
            if (timeToShutdown == 0)
            {
                Log.Info(this, $"Started. No shut down time set");
            }
            else
            {
                Log.Info(this, $"Started. Scheduled to shut down at {(DateTime.Now + TimeSpan.FromMilliseconds(timeToShutdown)).ToString("MMM-dd HH:mm")}");
                Shutdown.WaitOne(timeToShutdown);
                stop(0, "Hosted service stopped on shut down");
            }
        }

        private int getShutdownTime()
        {
            int milliseconds = 0;
            string shutdownTime = _configuration.GetValue<string>("shutdown");

            if (DateTime.TryParseExact(shutdownTime, "HHmm", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime time))
            {
                TimeSpan span = time - DateTime.Now;

                if (0 == (milliseconds = time <= DateTime.Now ? 0 : (int)span.TotalMilliseconds))
                {
                    stop(-1, $"Cannot start job after shut down time ({time})");
                }
            }
            return milliseconds;
        }
    }
}
