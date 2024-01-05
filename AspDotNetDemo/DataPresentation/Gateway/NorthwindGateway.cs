using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core7.Component;
using Clio.Demo.Core7.Gateway;
using Clio.Demo.Domain.Data.Northwind;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using Microsoft.Extensions.Configuration;

namespace Clio.Demo.DataPresentation.Gateway
{
    public sealed class NorthwindGateway : ServiceCore
    {
        private const string ApiName = "Northwind";

        public NorthwindGateway(IConfiguration configuration, IHttpClientFactory clientFactory) : base(configuration, clientFactory)
        {
        }

        public async Task<IEnumerable<T>> RetrieveData<T>() where T : class, new()
        {
            ApiHttpClient apiClient = new ApiHttpClient(ApiName, _clientFactory, _configuration);
            try
            {
                switch (new T())
                {
                    case Customer _:
                        return processResponse<T>(await apiClient.GetAll<Customer>("api/orders") as IHttpResponse<T[]>);
                    case Employee _:
                        return processResponse<T>(await apiClient.GetAll<Employee>("api/employees") as IHttpResponse<T[]>);
                    case Product _:
                        return processResponse<T>(await apiClient.GetAll<Product>("api/produts") as IHttpResponse<T[]>);
                    case Supplier _:
                        return processResponse<T>(await apiClient.GetAll<Supplier>("api/supppliers") as IHttpResponse<T[]>);
                    case Order _:
                        return processResponse<T>(await apiClient.GetAll<Order>("api/orders") as IHttpResponse<T[]>);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return CollectionEx.Empty<T>();
        }

        private IEnumerable<T> processResponse<T>(IHttpResponse<T[]> response)
        {
            if (response is null)
            {
                throw new ArgumentNullException($"{ApiName} returns NULL response on {typeof(T)} request");
            }
            if (response.Code != 200)
            {
                throw new Exception($"{response.Request}: {response.Code} {response.CodeName} {response.Message}");
            }
            Log.Block(this, (response.Data as T[]).Select(x => $"{x}"), $"{response.Request}");

            return response.Data;
        }
    }
}
