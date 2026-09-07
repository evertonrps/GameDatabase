using Asp.Versioning;
using GameDatabase.Domain.Exceptions;
using GameDatabase.Domain.Interfaces.Services;
using GameDatabase.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class SseController(ISseHub _sseHub) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Publish(
        [FromBody] MessageRequest request,
        CancellationToken cancellationToken)
    {
        await _sseHub.PublishAsync(
            request.Message,
            cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task Get(
        CancellationToken cancellationToken)
    {
        Response.ContentType = "text/event-stream";
        Response.Headers.CacheControl = "no-cache";
        Response.Headers.Connection = "keep-alive";

        var lastEventIdHeader =
            Request.Headers["Last-Event-ID"]
                .FirstOrDefault();

        long.TryParse(
            lastEventIdHeader,
            out var lastEventId);

        SseClientConnection connection;

        try
        {
            connection = _sseHub.Connect(lastEventId);
        }
        catch (SseHistoryNotAvailableException ex)
        {
            Response.StatusCode =
                StatusCodes.Status409Conflict;

            await Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message,
                    requestedEventId =
                        ex.RequestedEventId,
                    firstAvailableEventId =
                        ex.FirstAvailableEventId
                },
                cancellationToken);

            return;
        }

        await Response.StartAsync(cancellationToken);

        await Response.WriteAsync(
            ": connected\n\n",
            cancellationToken);

        await Response.Body.FlushAsync(
            cancellationToken);


        try
        {
            // Replay
            foreach (var message in connection.Replay)
                await WriteSseMessageAsync(
                    message,
                    cancellationToken);

            // Novas mensagens
            await foreach (var message
                           in connection.Reader.ReadAllAsync(
                               cancellationToken))
                await WriteSseMessageAsync(
                    message,
                    cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Cliente desconectou.
        }
        finally
        {
            _sseHub.Disconnect(connection.ClientId);
        }
    }


    private async Task WriteSseMessageAsync(
        NotifyMessage message,
        CancellationToken cancellationToken)
    {
        await Response.WriteAsync(
            $"id: {message.Id}\n",
            cancellationToken);

        await Response.WriteAsync(
            $"data: {message.Message}\n\n",
            cancellationToken);

        await Response.Body.FlushAsync(
            cancellationToken);
    }

    public sealed record MessageRequest(string Message);
}