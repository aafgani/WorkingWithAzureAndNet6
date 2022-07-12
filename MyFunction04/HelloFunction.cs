using System.Collections.Generic;
using System.Net;
using Infrastructure.Logging.Telemetry;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace MyFunction04
{
    public class HelloFunction
    {
        private readonly ILogger _logger;

        public HelloFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HelloFunction>();
        }

        [Function("HelloFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            FunctionTelemetry.SetRequestTelemetry();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
