using Mapping.GeolocationServices.IPLocationServices;
using System.Collections;

namespace Mapping.GeolocationServices.IPLocationServices.IPStack
{
    public class IPStackApiConfiguration : ApiConfiguration
    {
        public static readonly string ConfigurationKey = "IPStack";

        public static IPStackApiConfiguration FromConfiguration(IDictionary dictionary)
        {
            return new IPStackApiConfiguration(dictionary);
        }

        public IPStackApiConfiguration(IDictionary dictionary) : base(ConfigurationKey, dictionary)
        {
        }
    }
}