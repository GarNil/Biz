using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Common
{
    public static class ServiceHttpClient
    {
        private static HttpClientHandler GetClientHandler()
            => new HttpClientHandler
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true,
                AllowAutoRedirect = false
            };

        public static HttpResponseMessage Get(string uri)
            => GetAsync(uri).Result;

        public static async Task<HttpResponseMessage> GetAsync(string uri)
        {
            using (var httpClient = new HttpClient(GetClientHandler()) { Timeout = TimeSpan.FromMinutes(5) })
                return await httpClient.GetAsync(uri);
        }

        public static async Task<HttpResponseMessage> PostAsync(string uri, string data)
        {
            using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromMinutes(5) })
                return await httpClient.PostAsync(uri, new StringContent(data));
        }
    }
}
