using Microsoft.Azure.Functions.Worker;
using NotificationsFunction.Event;
using System.Text.Json;

namespace NotificationsFunction;

public class ProcessarPagamentoFunction
{
    private readonly ILogger _logger;

    public ProcessarPagamentoFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ProcessarPagamentoFunction>();
    }

    [Function("ProcessarPagamentoFunction")]
    public void Run([ServiceBusTrigger("payment-processed", Connection = "ServiceBusConnection")]  string message)
    {
        _logger.LogInformation("Mensagem recebida: {Message}", message);

        var payment = JsonSerializer.Deserialize<PaymentProcessedEvent>(message);

        if (payment == null)
        {
            _logger.LogWarning("Mensagem inválida recebida.");
            return;
        }

        if (payment.Status == "Aprovado")
        {
            _logger.LogInformation("[EMAIL] Compra aprovada com sucesso!");
        }
        else
        {
            _logger.LogInformation("[EMAIL] Compra não aprovada!");
        }
    }
}