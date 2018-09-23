using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mapping.GeolocationServices.IPLocationServices.IPStack
{
    public class IPStackLocationService : IPLocationService
    {
        private HttpClient _client;
        private IApiConfiguration _config;

        public IPStackLocationService(HttpClient client, IApiConfiguration config)
        {
            _client = client;
            _config = config;
            _client.BaseAddress = new Uri(_config.BaseAddress);
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
    }
}