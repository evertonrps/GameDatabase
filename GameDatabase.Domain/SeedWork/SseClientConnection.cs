using System.Threading.Channels;

namespace GameDatabase.Domain.SeedWork;

public sealed class SseClientConnection
{
    public required Guid ClientId { get; init; }

    public required ChannelReader<NotifyMessage> Reader { get; init; }

    public required IReadOnlyList<NotifyMessage> Replay { get; init; }
}