namespace GameDatabase.Domain.Exceptions;

public sealed class SseHistoryNotAvailableException : Exception
{
    public SseHistoryNotAvailableException(
        long requestedEventId,
        long firstAvailableEventId)
        : base(
            $"Event history is no longer available. " +
            $"Requested event ID: {requestedEventId}. " +
            $"First available event ID: {firstAvailableEventId}.")
    {
        RequestedEventId = requestedEventId;
        FirstAvailableEventId = firstAvailableEventId;
    }

    public long RequestedEventId { get; }

    public long FirstAvailableEventId { get; }
}