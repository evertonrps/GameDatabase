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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var games = await _platformService.GetAll();
        var ret = _mapper.Map<IEnumerable<PlatformModel>>(games);
        return Ok(ret);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _platformService.GetById(id);
        var ret = _mapper.Map<PlatformModel>(game);
        return Ok(ret);
    }

    [HttpPost]
    public async Task<IActionResult> NewGame(PlatformModel model)
    {
        var platform = _mapper.Map<Platform>(model);
        var created = await _platformService.CreatePlatform(platform);
        return Ok(created);
    }
}