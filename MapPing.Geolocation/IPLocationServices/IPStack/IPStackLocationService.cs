using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MapPing.Geolocation.IPLocationServices.IPStack
{
    public class IPStackLocationService : IPLocationService
    {
        static string ConfigurationKey = "IPLocation.IPStack";
        private HttpClient _client;
        private ApiConfiguration _config;

        public IPStackLocationService(HttpClient client, ApiConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<Position> GetPosition(string ipAddress)
        {
            var path = new Uri($"{ipAddress}?access_key={_config.ApiKey}", UriKind.Relative);
            var res = await _client.GetAsync(path);
            res.EnsureSuccessStatusCode();
            var ipStackResponse = await res.Content.ReadAsAsync<IPStackResponse>();

            return new Position
            {
                Latitude = ipStackResponse.latitude,
                Longitude = ipStackResponse.longitude,
                City = ipStackResponse.city,
                CountryCode = ipStackResponse.country_code,
                Region = ipStackResponse.region_name
            };
        }

        public static void HttpClientRegistration(HttpClient client, ApiConfiguration config)
        {
            client.BaseAddress = new Uri(config.BaseAddress);
        }
    }
}
