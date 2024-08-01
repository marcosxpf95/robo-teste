using Microsoft.AspNetCore.Mvc;
using Robo.Api.Mappings;
using Robo.Application.Services;
using Robo.Contracts.Requests;

namespace Robo.Api.Controllers;


[ApiController]
public class RoboController : ControllerBase
{
    private readonly IRoboServices _roboService;
    public RoboController(IRoboServices roboService)
    {
        _roboService = roboService;
    }

    [HttpGet(ApiEndpoints.Robo.Get)]
    public IActionResult Get()
    {
        var robo = _roboService.Get();

        if (robo is null)
            return NotFound();

        var response = robo.MatToResponse();
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Robo.Put)]
    public IActionResult Update([FromBody] RoboRequest request)
    {
        var result = _roboService.EnviarComando(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }
}
