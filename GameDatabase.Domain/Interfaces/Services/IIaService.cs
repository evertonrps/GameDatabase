namespace GameDatabase.Domain.Interfaces.Services;

public interface IIaService
{
    Task<string> ExecuteAsync(string prompt);
}