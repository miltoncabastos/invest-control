using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/investment-performance")]
public class InvestmentPerformanceController : ControllerBase
{
    [HttpGet]
    [Route("total-rendimentos-por-ativo")]
    public IActionResult ObterTodosRendimentosPorAtivo()
    {
        return Ok("Rotina em implementação");
    }
}