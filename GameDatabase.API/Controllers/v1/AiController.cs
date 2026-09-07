using Asp.Versioning;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabase.API.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly IIaService _service;

    public AiController(IIaService service)
    {
        _service = service;
    }

    [HttpPost("analisar")]
    public async Task<IActionResult> AnalisarDados([FromBody] string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt)) return BadRequest("Prompt cannot be null or whitespace");

        var result = await _service.ExecuteAsync(prompt);
        return Ok(result);
    }
}