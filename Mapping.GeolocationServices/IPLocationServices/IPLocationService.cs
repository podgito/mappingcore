using System.Threading.Tasks;

namespace Mapping.GeolocationServices.IPLocationServices
{
    public interface IPLocationService
    {
        Task<Position> GetPosition(string ipAddress);
    }
}