using Clio.Demo.Extension;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Abstraction.Data;
using Clio.Demo.Util.Telemetry.Seri;
using Clio.Demo.Core.Component.Service;

namespace Clio.Demo.Core.Component.Gateway
{
    public class ApiHttpClient : ServiceCore, IApiHttpClient
    {
        private string ApiName { get; set; }

        public ApiHttpClient(string apiName, IHttpClientFactory clientFactory, IConfiguration configuration) : base(configuration, clientFactory)
        {
            ApiName = apiName;
        }

        private HttpClient createHttpClient()
        {
            string baseAddress = _configuration.GetValue<string>($"{ApiName}:Address") ?? throw new Exception($"Cannot create '{ApiName}' Http client. Address configuration with that prefix is not found");

            HttpClient httpClient = _clientFactory.CreateClient(ApiName);

            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonConfig.MediaJson));

            return httpClient;
        }

        #region IApiClient

        public async Task<HttpResponse<T>> Get<T>(string route, string key) where T : class
        {
            HttpResponse<T> response = new HttpResponse<T>() { Request = "Get" };
            try
            {
                response.Data = await get<T>(response, route, key);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            finally
            {
                response.Done();
            }
            return response;
        }
        public async Task<HttpResponse<T[]>> GetAll<T>(string route) where T : class
        {
            HttpResponse<T[]> response = new HttpResponse<T[]>() { Request = "Get" };
            try
            {
                response.Data = await get<T[]>(response.Mark(), route);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            finally
            {
                response.Done();
            }
            return response;
        }
        public async Task<HttpResponse<T>> Post<T>(T data, string route) where T : class
        {
            HttpResponse<T> response = new HttpResponse<T>() { Request = "Post" };
            try
            {
                await update<T>(response.Mark(), data, route);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            finally
            {
                response.Done();
            }
            return response;
        }
        public async Task<HttpResponse<T>> Put<T>(T data, string route) where T : class
        {
            HttpResponse<T> response = new HttpResponse<T>() { Request = "Put" };
            try
            {
                await update<T>(response, data, route);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            finally
            {
                response.Done();
            }
            return response;
        }
        public async Task<HttpResponse<T>> Delete<T>(T data, string route) where T : class
        {
            HttpResponse<T> response = new HttpResponse<T>() { Request = "Delete" };
            try
            {
                await update<T>(response, data, route);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
                throw;
            }
            finally
            {
                response.Done();
            }
            return response;
        }

        private async Task<T> get<T>(IHttpResponse response, string route, string key = null) where T : class
        {
            T data = default(T);
            using (HttpClient httpClient = createHttpClient())
            {
                string address = httpClient.ComposeHttpAddress(route, key);

                using (HttpResponseMessage httpResponse = await httpClient.GetAsync(address))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        Stream stream = await httpResponse.Content.ReadAsStreamAsync();
                        data = await JsonSerializer.DeserializeAsync<T>(stream, JsonConfig.Options);
                    }
                    response.SetFrom(httpResponse, $"{response.Request} '{address}'");
                }
            }
            return data;
        }

        private async Task update<T>(IHttpResponse response, T data, string route) where T : class
        {
            string address = null;

            using (HttpClient httpClient = createHttpClient())
            {
                string json = JsonSerializer.Serialize<T>(data);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, JsonConfig.MediaJson);

                using (HttpResponseMessage httpResponse = await httpClient.PostAsync(address = httpClient.ComposeHttpAddress(route), httpContent))
                {
                    response.SetFrom(httpResponse, $"{response.Request} '{address}'");
                }
            }
        }

        #endregion IApiClient

        private static class JsonConfig
        {
            private static JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            public static JsonSerializerOptions Options => options;

            public const string MediaJson = "application/json";
            public const string MediaText = "text/plain";
            public const string Encoding  = "gzip";
            public const string Protocol  = "http";
        }
    }
}
