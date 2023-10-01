using System.Net.Http;

namespace Clio.Demo.Extension
{
	public static class HttpClientEx
	{
		public static string ComposeHttpAddress(this HttpClient httpClient, string route, string key = null, string protocol = "http")
		{
			return $"{protocol}://{($"{httpClient.BaseAddress.OriginalString}/{route}{(key is null ? "" : $"/{key}")}".Replace("//", "/"))}";
		}
	}
}
