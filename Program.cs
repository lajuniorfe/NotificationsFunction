using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var connectionString = builder.Configuration["RabbitMQConnection"];

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("================================");
    Console.WriteLine("RABBITMQ URI: configuração 'RabbitMQConnection' não encontrada.");
    Console.WriteLine("================================");
}
else
{
    try
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(connectionString)
        };

        await using var connection = await factory.CreateConnectionAsync();

        Console.WriteLine("================================");
        Console.WriteLine("RABBITMQ URI: OK");
        Console.WriteLine("================================");
    }
    catch (Exception ex)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"RABBITMQ URI: ERRO - {ex}");
        Console.WriteLine("================================");
    }
}

builder.Build().Run();
