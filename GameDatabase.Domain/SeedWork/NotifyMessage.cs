namespace GameDatabase.Domain.SeedWork;

public sealed record NotifyMessage(
    long Id,
    string Type,
    string Message);