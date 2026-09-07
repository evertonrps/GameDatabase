using System.Collections.Concurrent;
using System.Threading.Channels;
using GameDatabase.Domain.Exceptions;
using GameDatabase.Domain.Interfaces.Services;
using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Sse;

public sealed class SseHub : ISseHub
{
    private const int MaxHistory = 100;

    private readonly ConcurrentDictionary<
        Guid,
        Channel<NotifyMessage>> _clients = new();

    private readonly LinkedList<NotifyMessage> _history = new();

    private readonly object _sync = new();

    private long _messageId;

    public SseClientConnection Connect(long lastEventId)
    {
        var clientId = Guid.NewGuid();

        var channel = Channel.CreateUnbounded<NotifyMessage>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });

        lock (_sync)
        {
            // -------------------------------------------------
            // 1. Verifica se ainda temos histórico suficiente.
            // -------------------------------------------------

            if (lastEventId > 0 && _history.Count > 0)
            {
                var firstAvailableEventId =
                    _history.First!.Value.Id;

                if (lastEventId < firstAvailableEventId - 1)
                    throw new SseHistoryNotAvailableException(
                        lastEventId,
                        firstAvailableEventId);
            }

            // -------------------------------------------------
            // 2. Registra o cliente.
            //
            // A partir deste momento, qualquer mensagem
            // publicada será colocada no Channel dele.
            // -------------------------------------------------

            _clients.TryAdd(clientId, channel);

            // -------------------------------------------------
            // 3. Captura o ponto de corte.
            //
            // Tudo até este ID pertence ao replay.
            // Tudo depois dele chegará pelo Channel.
            // -------------------------------------------------

            var replayUntilEventId = _messageId;

            // -------------------------------------------------
            // 4. Obtém o replay.
            // -------------------------------------------------

            var replay = _history
                .Where(x =>
                    x.Id > lastEventId &&
                    x.Id <= replayUntilEventId)
                .ToList();

            return new SseClientConnection
            {
                ClientId = clientId,
                Reader = channel.Reader,
                Replay = replay
            };
        }
    }

    public void Disconnect(Guid clientId)
    {
        if (_clients.TryRemove(
                clientId,
                out var channel))
            channel.Writer.TryComplete();
    }

    public Task PublishAsync(
        string message,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_sync)
        {
            // ---------------------------------------------
            // Gera o ID dentro do mesmo lock usado pelo
            // Connect().
            // ---------------------------------------------

            var id = ++_messageId;

            var sseMessage = new NotifyMessage(
                id,
                "default",
                message);

            // ---------------------------------------------
            // Guarda no histórico.
            // ---------------------------------------------

            _history.AddLast(sseMessage);

            while (_history.Count > MaxHistory) _history.RemoveFirst();

            // ---------------------------------------------
            // Broadcast.
            //
            // Como estamos dentro do mesmo lock do Connect(),
            // garantimos a ordem entre:
            //
            //   Connect()
            //   Publish()
            //
            // ---------------------------------------------

            foreach (var client in _clients.Values) client.Writer.TryWrite(sseMessage);
        }

        return Task.CompletedTask;
    }

    public int ConnectedClients =>
        _clients.Count;
}