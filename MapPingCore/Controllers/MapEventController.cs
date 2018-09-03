using MapPing.Geolocation.IPLocationServices;
using MapPingCore.Common.Models;
using MapPingCore.Hubs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MapPingCore.Controllers
{
    [Produces("application/json")]
    [Route("api/ping")]
    public class PingController : Controller
    {
        private readonly MapHubService _hubService;
        private readonly IPLocationService _locationService;

        public PingController(MapHubService mapHubService, IPLocationService locationService)
        {
            _hubService = mapHubService;
            _locationService = locationService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Ping([FromBody]Ping ping)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);

            //var clientAddress = this.HttpContext.Connection.RemoteIpAddress;
            var clientAddress = "185.104.217.66"; // testing

            var position = await _locationService.GetPosition(clientAddress.ToString());
            //todo: map position to ping

            ping.Latitude = position.Latitude;
            ping.Longitude = position.Longitude;

            await _hubService.SendPingToAll(ping);
            return Ok();
        }



    }
}