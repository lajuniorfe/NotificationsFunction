namespace NotificationsFunction.Event
{
    public record UserCreatedEvent(
    Guid UserId,
    string Name,
    string Email
);
}
