using Clio.Demo.Extension;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.Core.Component.Service
{
    public class ServiceCore
    {
        protected IConfiguration _configuration;
        protected IHttpClientFactory _clientFactory;

        public ServiceCore(IConfiguration configuration)
        {
            configuration.Inject(out _configuration);
        }
        public ServiceCore(IConfiguration configuration, IHttpClientFactory clientFactory) : this(configuration)
        {
            clientFactory.Inject(out _clientFactory);
        }
    }
}
