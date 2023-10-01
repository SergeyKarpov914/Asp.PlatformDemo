using Clio.Demo.Abstraction.Interface;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Clio.Demo.Abstraction.Data
{
    public class HttpResponse<T> : IHttpResponse<T> where T : class
    {
        public T Data { get; set; }

        public int    Code     { get; set; }
        public string CodeName { get; set; }
        public string Request  { get; set; }
        public string Message  { get; set; }

        public TimeSpan Elapsed => _stopwatch.Elapsed;

        public IHttpResponse SetFrom(HttpResponseMessage httpResponse, string request = null)
        {
            if (null != httpResponse)
            {
                Code     = (int)httpResponse.StatusCode;
                CodeName = httpResponse.StatusCode.ToString();
                Message  = httpResponse.ReasonPhrase;
                Request  = request;
            }
            return this;
        }

        private Stopwatch _stopwatch = new Stopwatch();

        public IHttpResponse Mark()
        {
            _stopwatch.Start();
            return this;
        }
        public void Done()
        {
            _stopwatch.Stop();
        }
    }
}
