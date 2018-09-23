namespace Mapping.GeolocationServices.IPLocationServices
{
    public interface IApiConfiguration
    {
        string ApiKey { get; }
        string BaseAddress { get; }
        string PathFormat { get; }
    }
}