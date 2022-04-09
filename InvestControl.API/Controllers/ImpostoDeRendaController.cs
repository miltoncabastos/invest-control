using InvestControl.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/imposto-de-renda")]
    public class ImpostoDeRendaController : ControllerBase
    {
        private readonly IImpostoDeRendaService _impostoDeRendaService;

        public ImpostoDeRendaController(IImpostoDeRendaService impostoDeRendaService)
        {
            _impostoDeRendaService = impostoDeRendaService;
        }

        [HttpGet]
        [Route("imposto-anual-custodia/{ano}")]
        public IActionResult ImpostoDeRendaAnualCustodia(int ano)
        {
            return Ok(_impostoDeRendaService.CalcularCustodiaAnual(ano));
        }

        [HttpGet]
        [Route("imposto-mesal-lucros-prejuizos/{ano}")]
        public IActionResult ImpostoDeRendaMensalLucrosEPrejuizos(int ano)
        {
            return Ok(_impostoDeRendaService.CalcularLucroEPrejuizoMensal(ano));
        }

    }
}
