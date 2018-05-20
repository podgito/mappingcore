using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapPingCore.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MapPingCore.Controllers
{
    [Produces("application/json")]
    [Route("api/MapEvent")]
    public class MapEventController : Controller
    {
        private readonly IHubContext<MapHub> _hubContext;

        public MapEventController(IHubContext<MapHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("test")]
        public Task Test()
        {
            return _hubContext.Clients.All.SendAsync("event", "hello!!!");
        }

    }
}