using MapPing.Geolocation.IPLocationServices;
using MapPing.Geolocation.IPLocationServices.IPStack;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapPing.Tests.Geolocation
{
    [Category("Integration")]
    public class IPStackTest
    {
        IPStackLocationService service;
        const string GoogleIP = "8.8.8.8";
        [SetUp]
        public void Setup()
        {
            var configuration = new ApiConfiguration
            {
                BaseAddress = "http://api.ipstack.com/",
                ApiKey = "0b50b6896a303625ef928bf7c058c1c8"
            };
            var client = new HttpClient();
            IPStackLocationService.HttpClientRegistration(client, configuration);
            service = new IPStackLocationService(client, configuration);
        }



        [Test]
        public async Task BasicIPLookup()
        {
            //Act
            var response = await service.GetPosition(GoogleIP);

            //Assert
            Assert.AreEqual("US", response.CountryCode);
        }
    }
}