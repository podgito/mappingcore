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

        public PingController(MapHubService mapHubService)
        {
            _hubService = mapHubService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Ping([FromBody]Ping ping)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            
            await _hubService.SendPingToAll(ping);
            return Ok();
        }
    }
}