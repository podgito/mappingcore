using Mapping.GeolocationServices.IPLocationServices;
using Mapping.GeolocationServices.IPLocationServices.IPStack;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mapping.Tests.Geolocation
{
    [Category("Integration")]
    public class IPStackTest
    {
        private IPStackLocationService service;
        private const string GoogleIP = "8.8.8.8";

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IApiConfiguration>();
            configuration.SetupGet(c => c.BaseAddress).Returns("http://api.ipstack.com/");
            configuration.SetupGet(c => c.ApiKey).Returns("0b50b6896a303625ef928bf7c058c1c8");

            //{
            //    BaseAddress = "http://api.ipstack.com/",
            //    ApiKey = "0b50b6896a303625ef928bf7c058c1c8"
            //};
            var client = new HttpClient();
            service = new IPStackLocationService(client, configuration.Object);
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