using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();


// Logger temporário para validar a inicialização
using var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
});

var logger = loggerFactory.CreateLogger("Startup");

logger.LogInformation("================================");
logger.LogInformation("PROGRAM.CS FOI EXECUTADO");
logger.LogInformation("================================");


builder.Build().Run();
