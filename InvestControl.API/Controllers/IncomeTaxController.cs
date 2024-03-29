﻿using InvestControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/income-tax")]
    public class IncomeTaxController : ControllerBase
    {
        private readonly IImpostoDeRendaService _impostoDeRendaService;

        public IncomeTaxController(IImpostoDeRendaService impostoDeRendaService)
        {
            _impostoDeRendaService = impostoDeRendaService;
        }

        [HttpGet]
        [Route("custodia/{ano}")]
        public IActionResult Custodia(int ano)
        {
            return Ok(_impostoDeRendaService.CalcularCustodiaAnual(ano));
        }

        [HttpGet]
        [Route("calcular-lucros-prejuizos-mensais/{ano}")]
        public IActionResult ImpostoDeRendaMensalLucrosEPrejuizos(int ano)
        {
            return Ok(_impostoDeRendaService.CalcularLucroOuPrejuizoMensal(ano));
        }
        
        [HttpGet]
        [Route("calcular-imposto-pagar-mensal/{ano}")]
        public IActionResult CalcularImpostoAPagarMensal(int ano)
        {
            return Ok(_impostoDeRendaService.CalcularImpostoAPagarMensal(ano));
        }

    }
}
