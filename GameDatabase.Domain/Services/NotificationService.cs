using GameDatabase.Domain.Interfaces.Services;
using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.Services;

public class NotificationService(ISseHub hub) : INotificationService
{
    public async Task<bool> Notify(NotifyMessage message)
    {
        await hub.PublishAsync(message.Message);
        return true;
    }
}