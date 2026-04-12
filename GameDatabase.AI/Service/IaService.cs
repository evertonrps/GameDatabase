using System.ComponentModel;
using System.Text.Json;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OllamaSharp;

namespace GameDatabase.AI.Service;

public class IaService : IIaService
{
    private readonly IDeveloperService _developerService;
    private readonly IGameService _gameService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IaService> _logger;
    private readonly AIAgent _agent;

    public IaService(IDeveloperService developerService, IGameService gameService, IConfiguration configuration, ILogger<IaService> logger)
    {
        _developerService = developerService;
        _gameService = gameService;
        _configuration = configuration;
        _logger = logger;
        var url = new Uri(_configuration["AISettings:Url"] ?? "");
        var model = _configuration["AISettings:Model"];
        IChatClient ollamaClient = new OllamaApiClient(url, model ?? "llama3");

        _agent = ollamaClient
            .AsBuilder()
            .BuildAIAgent(
                "You are a video game expert, knowledgeable about developers, publishers, and games.",
                "Player One",
                tools:
                [
                    AIFunctionFactory.Create(AddDeveloper, nameof(AddDeveloper)),
                    AIFunctionFactory.Create(GetGamesByDeveloper, nameof(GetGamesByDeveloper)),
                    AIFunctionFactory.Create(GetAllDevelopers, nameof(GetAllDevelopers)),
                    AIFunctionFactory.Create(GetDeveloperByName, nameof(GetDeveloperByName))
                ])
            .AsBuilder()
            .Use(LogFunctionNameMiddleware)
            .Build();
    }


    public async Task<string> ExecuteAsync(string prompt)
    {
        var agentRunResponse = await _agent.RunAsync(prompt);
        return agentRunResponse.Text;
    }

    private async ValueTask<object?> LogFunctionNameMiddleware(AIAgent agent, FunctionInvocationContext context,
        Func<FunctionInvocationContext, CancellationToken, ValueTask<object?>>? next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"LogFunctionNameMiddleware - function invoked: {context.Function.Name}");
        return await next(context, cancellationToken);
    }

    /// <summary>
    ///     Persists a new developer via the injected service.
    /// </summary>
    [Description("Add a new developer to the database")]
    private async Task<string?> AddDeveloper(
        [Description("Name of the developer")] string name,
        [Description("Founded date of developer")]
        DateTime founded,
        [Description("Website of the developer")]
        string website)
    {
        var developer = Developer.Factory(name, founded, website);
        await _developerService.CreateDeveloper(developer); // Chamada ao service
        return $"Developer '{name}' with id {developer.Id} added successfully.";
    }

    /// <summary>
    ///     Retrieves all games belonging to a specific developer.
    /// </summary>
    [Description("Get all games of a developer")]
    private async Task<string?> GetGamesByDeveloper(
        [Description("Developer ID")] string developerId)
    {
        var games = await _gameService.GetByDeveloperId(developerId);
        if (games == null || !games.Any())
            return $"No games found for developer with id {developerId}.";

        // For simplicity, return a comma‑separated list of game titles.
        var titles = string.Join(", ", games.Select(g => g.Title));
        return $"Games for developer {developerId}: {titles}.";
    }

    /// <summary>
    ///     Retrieves all registered developers.
    ///     The return value is a JSON array of objects containing the developer Id and Name,
    ///     which is useful for the AI agent, while also including a human‑readable
    ///     message that can be shown directly to users.
    /// </summary>
    [Description("Get all developers in the database")]
    private async Task<string?> GetAllDevelopers()
    {
        var developers = await _developerService.GetAll();
        if (developers == null || !developers.Any())
            return "No developers found.";

        // Build a lightweight DTO for each developer
        var dto = developers.Select(d => new { d.Id, d.Name }).ToList();

        // Serialize to JSON for the AI agent to parse
        var json = JsonSerializer.Serialize(dto);

        // Human‑readable list for console or UI
        var names = string.Join(", ", developers.Select(d => d.Name));

        return $"Developers: {names}. JSON: {json}";
    }

    /// <summary>
    ///     Retrieves a developer by name.
    /// </summary>
    [Description("Get a developer by name")]
    private async Task<string?> GetDeveloperByName(
        [Description("Developer name")] string name)
    {
        var developer = await _developerService.GetByName(name);

        if (developer == null)
            return $"Developer '{name}' was not found.";

        return JsonSerializer.Serialize(new
        {
            developer.Id,
            developer.Name,
            developer.Founded,
            developer.WebSite
        });
    }
}