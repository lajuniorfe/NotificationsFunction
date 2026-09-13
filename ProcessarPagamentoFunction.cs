using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotificationsFunction.Event;
using System.Text.Json;

namespace NotificationsFunction;

public class ProcessarPagamentoFunction
{
    private readonly ILogger _logger;
    private const string QueueConnectionSetting = "RabbitMQConnection";

    public ProcessarPagamentoFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ProcessarPagamentoFunction>();
    }

    [Function("ProcessarPagamentoFunction")]
    public void Run([RabbitMQTrigger("payment-processed", ConnectionStringSetting = QueueConnectionSetting)] string message)
    {
        var payment = JsonSerializer.Deserialize<PaymentProcessedEvent>(message);

        if (payment == null)
        {
            _logger.LogWarning("Mensagem inválida recebida.");
            return;
        }

        if (payment.Status == "Aprovado")
        {
            _logger.LogInformation(
                "[EMAIL] Compra aprovada com sucesso!");
        }
        else
        {
            _logger.LogInformation(
                "[EMAIL] Compra não aprovada!");
        }
    }
}