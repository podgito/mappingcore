using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MapPing.Geolocation.IPLocationServices.IPStack
{
    public class IPStackLocationService : IPLocationService
    {
        static string ConfigurationKey = "IPLocation.IPStack";
        public static string HttpClientConfigName = "IPStack";

        public Position GetPosition(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public static void HttpClientRegistration(HttpClient client, ApiConfiguration config)
        {
            client.BaseAddress = new Uri("https://someapiurl/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "MyCustomUserAgent");
        }
    }
}
