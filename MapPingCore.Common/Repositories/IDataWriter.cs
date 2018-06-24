using MapPingCore.Common.Models;
using System.Threading.Tasks;

namespace MapPingCore.Common.Repositories
{
    public interface IDataWriter
    {
        Task StorePing(Ping ping);
    }
}