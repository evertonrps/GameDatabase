using Asp.Versioning;
using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService, IMapper mapper)
    {
        _platformService = platformService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get a list of all game platforms.
    /// </summary>
    /// <returns>All game platforms as a list.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var games = await _platformService.GetAll();
        var ret = _mapper.Map<IEnumerable<PlatformModel>>(games);
        return Ok(ret);
    }

    /// <summary>
    /// Get a game platform by its ID.
    /// </summary>
    /// <param name="id">The ID of the game platform.</param>
    /// <returns>The game platform.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _platformService.GetById(id);
        var ret = _mapper.Map<PlatformModel>(game);
        return Ok(ret);
    }

    /// <summary>
    /// Create a new game platform.
    /// </summary>
    /// <param name="model">The game platform model.</param>
    /// <returns>Newly created game platform.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NewGame(PlatformModel model)
    {
        var platform = _mapper.Map<Platform>(model);
        var created = await _platformService.CreatePlatform(platform);
        return Ok(created);
    }
}