using Microsoft.Azure.Functions.Worker;
using NotificationsFunction.Event;
using System.Text.Json;

namespace NotificationsFunction
{
    public class UsuarioCriadoFunction
    {
        private readonly ILogger<UsuarioCriadoFunction> _logger;

        public UsuarioCriadoFunction(ILogger<UsuarioCriadoFunction> logger)
        {
            _logger = logger;
        }

        [Function("UsuarioCriadoFunction")] 
        public void Run([RabbitMQTrigger("user-created", ConnectionStringSetting = "RabbitMQConnection")] string message) 
        { 
            _logger.LogInformation("Mensagem de usuário criado recebida: {Message}", message); 
            var usuario = JsonSerializer.Deserialize<UserCreatedEvent>(message); 
            if (usuario == null) 
            {
                _logger.LogWarning("Mensagem de usuário inválida recebida."); 
                return; 
            } 
            _logger.LogInformation("[EMAIL] Usuário criado com sucesso! UserId: {UserId}", usuario.UserId); 
        }
    }
}
