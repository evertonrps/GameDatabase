using Asp.Versioning;
using GameDatabase.API.MapperExtensions;
using GameDatabase.API.ViewModels;
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

    // GET api/v1/developer/{id}
    /// <summary>
    ///     Retrieves information from a developer by identifier.
    /// </summary>
    /// <param name="id">Unique developer identifier.</param>
    /// <returns>Developer details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeveloperOutputModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeveloperById(string id)
    {
        var developer = await _developerService.GetById(id);
        var result = developer.ToViewModel();
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    // GET api/v1/developer
    /// <summary>
    ///     Lists all registered developers.
    /// </summary>
    /// <returns>A collection of developers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeveloperOutputModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDevelopers()
    {
        var developers = await _developerService.GetAll();
        var result = developers.ToViewModelList();
        return Ok(result);
    }

    // POST api/v1/developer
    /// <summary>
    ///     Create a new developer.
    /// </summary>
    /// <param name="inputModel">Model</param>
    /// <returns>Developer information created.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeveloperOutputModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> NewDeveloper(DeveloperInputModel inputModel)
    {
        var developer = inputModel.ToEntity();
        var created = await _developerService.CreateDeveloper(developer);
        if (created.Erros.Any()) return BadRequest(created.Erros.Select(x => x.ErrorMessage));

        var developerOutput = created.ToViewModel();
        return Ok(developerOutput);
    }
}