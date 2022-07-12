using Infrastructure.Logging.Telemetry;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => {
        s.AddSingleton<ITelemetryInitializer, FunctionTelemetry>();
    })
    .Build();

host.Run();