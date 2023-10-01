using Clio.Demo.Core.Data;

namespace Clio.Demo.Core.Interface
{
    public interface IApiHttpClient
    {
        Task<HttpResponse<T>>   Post  <T>(T      data,  string route) where T : class;
        Task<HttpResponse<T>>   Get   <T>(string route, string key  ) where T : class;
        Task<HttpResponse<T[]>> GetAll<T>(string route              ) where T : class;
        Task<HttpResponse<T>>   Delete<T>(T      data,  string route) where T : class;
    }
}
