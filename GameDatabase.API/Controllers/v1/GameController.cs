using Asp.Versioning;
using GameDatabase.API.MapperExtensions;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    ///     Get a list of all games.
    /// </summary>
    /// <returns>All games as a list.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var games = await _gameService.GetAll();
        var ret = games.ToViewModelList();
        return Ok(ret);
    }

    /// <summary>
    ///     Get a game by its ID.
    /// </summary>
    /// <param name="id">The ID of the game.</param>
    /// <returns>The game.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var game = await _gameService.GetById(id);
        var ret = game.ToViewModel();
        return Ok(ret);
    }

    /// <summary>
    ///     Get all games for a specific developer.
    /// </summary>
    /// <param name="developerId">The ID of the developer.</param>
    /// <returns>List of games belonging to the developer.</returns>
    [HttpGet("developer/{developerId}/games")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByDeveloper(string developerId)
    {
        var games = await _gameService.GetByDeveloperId(developerId);
        if (games == null || !games.Any())
            return NotFound();

        var ret = games.ToViewModelList();
        return Ok(ret);
    }

    /// <summary>
    ///     Create a new game.
    /// </summary>
    /// <param name="model">The game model.</param>
    /// <returns>Newly created game.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NewGame(GameModel model)
    {
        var game = model.ToEntity();
        var created = await _gameService.CreateGame(game);
        return Ok(created);
    }
}