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
public class DeveloperController : ControllerBase
{
    private readonly IDeveloperService _developerService;
    private readonly IMapper _mapper;
    private readonly IDeveloperRepository _repository;

    public DeveloperController(IDeveloperService developerService, IMapper mapper)
    {
        _developerService = developerService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeveloperById(int id)
    {
        var developers = await _developerService.GetById(id);
        var result = _mapper.Map<DeveloperModel>(developers);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        var developers = await _developerService.GetAll();
        var result = _mapper.Map<IEnumerable<DeveloperModel>>(developers);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NewDeveloper(DeveloperModel model)
    {
        var developer = _mapper.Map<Developer>(model);
        var created = await _developerService.CreateDeveloper(developer);
        return Ok(created);
    }
}