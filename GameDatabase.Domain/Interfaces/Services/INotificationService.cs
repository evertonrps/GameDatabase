using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.Interfaces.Services;

public interface INotificationService
{
    Task<bool> Notify(NotifyMessage message);
}