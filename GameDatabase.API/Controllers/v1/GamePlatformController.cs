using Asp.Versioning;
using GameDatabase.API.MapperExtensions;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class GamePlatformController : ControllerBase
{
    private readonly IGamePlatformService _gameService;

    public GamePlatformController(IGamePlatformService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    ///     Get a list of all game platforms.
    /// </summary>
    /// <returns>All game platforms as a list.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var games = await _gameService.GetAll();
        var ret = games.ToViewModelList();
        return Ok(ret);
    }

    /// <summary>
    ///     Get a game platform by its ID.
    /// </summary>
    /// <param name="id">The ID of the game platform.</param>
    /// <returns>The game platform.</returns>
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
    ///     Create a new game platform.
    /// </summary>
    /// <param name="model">The game platform model.</param>
    /// <returns>Newly created game platform.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NewGamePlatform(GamePlatformModel model)
    {
        var game = model.ToEntity();
        var created = await _gameService.CreateGamePlatform(game);
        return Ok(created);
    }
}