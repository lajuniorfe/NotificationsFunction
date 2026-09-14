using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var connectionString = builder.Configuration["RabbitMQConnection"];

// Logger temporário para validar a inicialização
using var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
});

var logger = loggerFactory.CreateLogger("Startup");

logger.LogInformation("================================");
logger.LogInformation("PROGRAM.CS FOI EXECUTADO");
logger.LogInformation("================================");


logger.LogInformation(
    "RabbitMQConnection configurado: {Configurado}",
    !string.IsNullOrWhiteSpace(connectionString));

if (!string.IsNullOrWhiteSpace(connectionString))
{
    try
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(connectionString)
        };

        await using var connection =
            await factory.CreateConnectionAsync();

        logger.LogInformation("================================");
        logger.LogInformation("RABBITMQ URI: CONEXÃO OK");
        logger.LogInformation("================================");
    }
    catch (Exception ex)
    {
        logger.LogError(
            ex,
            "RABBITMQ URI: ERRO AO CONECTAR");
    }
}
else
{
    logger.LogError(
        "RABBITMQ URI: configuração 'RabbitMQConnection' não encontrada.");
}
builder.Build().Run();
