using Mapping.GeolocationServices.IPLocationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            var ipAddress = GetIpFromRequestHeaders(req);

            var ipServiceConfig = Environment.GetEnvironmentVariable("IPGeolocationServices.IPStack.APIKey");

            var kernel = DIContainer.GetKernel();
            var ipLocationService = kernel.Get<IPLocationService>();

            var position = await ipLocationService.GetPosition(ipAddress);

            var ping = new { latitude = position.Latitude, longitude = position.Longitude, value = 10 };

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
                    Arguments = new[] { ping }
                });

            return new OkResult();
        }

        [FunctionName("negotiate")]
        public static IActionResult Negotiate([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequest req,
                                        [SignalRConnectionInfo(HubName = hubName)]SignalRConnectionInfo info,
                                        TraceWriter log)
        {
            return new OkObjectResult(info);
        }

        private static string GetIpFromRequestHeaders(HttpRequest request)
        {

            if (request.Headers.TryGetValue("X-Forwarded-For", out var values))
            {
                return values.FirstOrDefault().Split(new char[] { ',' }).FirstOrDefault().Split(new char[] { ':' }).FirstOrDefault();
            }

#if DEBUG
            return "176.250.133.195";
#endif

            return "";
        }
    }
}