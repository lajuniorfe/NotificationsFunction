namespace NotificationsFunction.Event
{
    public record PaymentProcessedEvent(
    Guid UserId,
    Guid GameId,
    string Status
);
}
