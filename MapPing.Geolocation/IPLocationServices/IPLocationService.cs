using System.Threading.Tasks;

namespace MapPing.Geolocation.IPLocationServices
{
    public interface IPLocationService
    {
        Task<Position> GetPosition(string ipAddress);
    }
}