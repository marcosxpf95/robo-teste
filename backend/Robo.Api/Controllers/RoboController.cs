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

    [HttpPut(ApiEndpoints.Robo.InclicarCabeca)]
    public IActionResult InclinarCabeca([FromBody] RoboRequest request)
    {
        var result = _roboService.InclinarCabeca(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }

    [HttpPut(ApiEndpoints.Robo.RotacionarCabeca)]
    public IActionResult RotacionarCabeca([FromBody] RoboRequest request)
    {
        var result = _roboService.RotacionarCabeca(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }

    [HttpPut(ApiEndpoints.Robo.ContrairBracoDireito)]
    public IActionResult ContrairBracoDireito([FromBody] RoboRequest request)
    {
        var result = _roboService.ContrairBracoDireito(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }

    [HttpPut(ApiEndpoints.Robo.RatacionarBracoDireito)]
    public IActionResult RotacionarBracoDireito([FromBody] RoboRequest request)
    {

        var result = _roboService.RatacionarBracoDireito(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }

    [HttpPut(ApiEndpoints.Robo.ContrairBracoEsquerdo)]
    public IActionResult ContrairBracoEsquerdo([FromBody] RoboRequest request)
    {
        var result = _roboService.ContrairBracoEsquerdo(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }

    [HttpPut(ApiEndpoints.Robo.RatacionarBracoEsquerdo)]
    public IActionResult RotacionarBracoEsquerdo([FromBody] RoboRequest request)
    {
        var result = _roboService.RatacionarBracoEsquerdo(request!.Comando);

        if (!result.Success)
            return BadRequest(result);

        return Ok();
    }
}
