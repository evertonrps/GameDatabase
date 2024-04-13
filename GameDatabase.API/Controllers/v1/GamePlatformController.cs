
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GameDatabase.API.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class GamePlatformController : ControllerBase
{
    
    private readonly IGamePlatformService _gameService;
    private readonly IMapper _mapper;

    public GamePlatformController(IGamePlatformService gameService, IMapper mapper)        
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var games = await _gameService.GetAll();
        var ret = _mapper.Map<IEnumerable<GamePlatformModel>>(games);
        return Ok(ret);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _gameService.GetById(id);
        var ret = _mapper.Map<GamePlatformModel>(game);
        return Ok(ret);
    }

    [HttpPost]
    public async Task<IActionResult> NewGamePlatform(GamePlatformModel model)
    {
        var game = _mapper.Map<GamePlatform>(model);
        bool created = await _gameService.CreateGamePlatform(game);
        return Ok(created);
    }
}


