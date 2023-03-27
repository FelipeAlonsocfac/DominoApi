using Domino.Api.Application.Interfaces;
using Domino.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domino.Api.Controllers;

[ApiVersion("1.0")]
[Route("/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class DominoController : ControllerBase
{
    private readonly IDominoService _dominoService;

    public DominoController(IDominoService dominoService)
    {
        _dominoService = dominoService;
    }

    /// <summary>
    /// This method tries to sort dominoes.
    /// </summary>
    /// <param name="dominoes"></param>
    /// <returns></returns>
    [HttpPost("sort")]
    [DominoFilter]
    public async Task<IActionResult> Sort([FromBody] List<string> dominoes)
    {
        var result = await _dominoService.Sort(dominoes);

        return Ok(result);
    }
}
