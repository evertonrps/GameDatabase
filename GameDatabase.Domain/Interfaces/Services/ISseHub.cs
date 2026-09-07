using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.Interfaces.Services;

public interface ISseHub
{
    int ConnectedClients { get; }
    SseClientConnection Connect(long lastEventId);

    void Disconnect(Guid clientId);

    Task PublishAsync(
        string message,
        CancellationToken cancellationToken = default);
}