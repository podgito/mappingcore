using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mapping.Function
{
    public static class Functions
    {
        private const string hubName = "events";
        private const string target = "event";

        [FunctionName("message")]
        public static async Task<IActionResult> PostMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req,
            [SignalR(HubName = hubName)]IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            JToken bodyObject;
            try
            {
                bodyObject = JToken.ReadFrom(new JsonTextReader(new StreamReader(req.Body)));
            }
            catch (Exception ex)
            {
                log.LogCritical(ex, "Error parsing Json");
                return new BadRequestObjectResult(ex.ToString());
            }

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = target,
                    Arguments = new[] { bodyObject }
                });

            return new OkResult();
        }

        [FunctionName("negotiate")]
        public static IActionResult Negotiate([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequest req,
                                        [SignalRConnectionInfo(HubName = hubName)]SignalRConnectionInfo info,
                                        TraceWriter log)
        {
            //var info = new AzureSignalRConnectionInfo
            //{
            //    AccessKey = "NWMtdEmEg5je0PaVM75kA8q43sDewwI7bujTf1Av/pw=",
            //    Endpoint = "https://mapping.service.signalr.net"
            //};
            return new OkObjectResult(info);
        }
    }
}