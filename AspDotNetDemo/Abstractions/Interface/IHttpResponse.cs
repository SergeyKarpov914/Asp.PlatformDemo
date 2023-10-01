using System;
using System.Net.Http;

namespace Clio.Demo.Abstraction.Interface
{
    public interface IHttpResponse
    {
        int      Code     { get; }
        string   CodeName { get; }
        string   Request  { get; }
        string   Message  { get; }
        TimeSpan Elapsed  { get; }

        IHttpResponse SetFrom(HttpResponseMessage httpResponse, string request);
    }

    public interface IHttpResponse<T> : IHttpResponse where T : class
    {
        T Data { get; }
    }
}
