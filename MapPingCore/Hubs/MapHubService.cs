using MapPingCore.Common.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MapPingCore.Hubs
{
    public class MapHubService
    {
        private readonly IHubContext<MapHub> _hubContext;

        public MapHubService(IHubContext<MapHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendPingToAll(Ping ping)
        {
            return _hubContext.Clients.All.SendAsync("event", ping);
        }
    }
}