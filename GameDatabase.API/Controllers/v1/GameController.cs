using Asp.Versioning;
using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;
    private readonly IGameRepository _repository;

    public GameController(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var games = await _gameService.GetAll();
        var ret = _mapper.Map<IEnumerable<GameModel>>(games);
        return Ok(ret);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _gameService.GetById(id);
        var ret = _mapper.Map<GameModel>(game);
        return Ok(ret);
    }

    [HttpPost]
    public async Task<IActionResult> NewGame(GameModel model)
    {
        var game = _mapper.Map<Game>(model);
        var created = await _gameService.CreateGame(game);
        return Ok(created);
    }
}