using System.Collections;

namespace Mapping.GeolocationServices.IPLocationServices
{
    public abstract class ApiConfiguration : IApiConfiguration
    {
        private readonly string geolocationServiceName;
        private readonly IDictionary dictionary;

        public ApiConfiguration(string geolocationServiceName, IDictionary dictionary)
        {
            this.geolocationServiceName = geolocationServiceName;
            this.dictionary = dictionary;
        }

        public string ApiKey
        {
            get { return GetDictionaryValue("ApiKey"); }
        }

        public string BaseAddress
        {
            get { return GetDictionaryValue("BaseAddress"); }
        }
        public string PathFormat
        {
            get { return GetDictionaryValue("PathFormat"); }
        }

        private string CreateKey(string propName)
        {
            return $"IPGeolocationServices.{geolocationServiceName}.{propName}";
        }

        private string GetDictionaryValue(string propName)
        {
            return dictionary[CreateKey(propName)].ToString();
        }
    }
}