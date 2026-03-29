using Asp.Versioning;
using AutoMapper;
using GameDatabase.API.MapperExtensions;
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

    public DeveloperController(IDeveloperService developerService)
    {
        _developerService = developerService;
    }

    /// <summary>
    /// Get Developer by id
    /// </summary>
    /// <param name="id"></param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeveloperOutputModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDeveloperById(int id)
    {
        var developer = await _developerService.GetById(id);
        var result = developer.ToViewModel();
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    /// <summary>
    ///  List all developers 
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeveloperOutputModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        var developers = await _developerService.GetAll();
        var result = developers.ToViewModelList();
        return Ok(result);
    }

    /// <summary>
    /// Create a new developer
    /// </summary>
    /// <param name="inputModel">DeveloperModel</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeveloperOutputModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> NewDeveloper(DeveloperInputModel inputModel)
    {
        var developer = inputModel.ToEntity();
        var created = await _developerService.CreateDeveloper(developer);
        if (created.Erros.Any())
        {
            return BadRequest(created.Erros.Select(x=> x.ErrorMessage));
        }

        var developerOutput = created.ToViewModel();
        return Ok(developerOutput);
    }
}