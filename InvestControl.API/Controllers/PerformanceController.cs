using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("performance")]
public class PerformanceController : ControllerBase
{
    [HttpGet("/total-rendimentos-por-ativo")]
    public IActionResult ObterTodosRendimentosPorAtivo()
    {
        return Ok("Rotina em implementação");
    }
}