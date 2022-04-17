using System.Linq;
using InvestControl.Domain.Entity;
using InvestControl.Infra.Context;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/dados")]
    public class DadosController : ControllerBase
    {
        [HttpGet]
        [Route("obter-corretoras")]
        public IActionResult ObterTodasCorretoras([FromServices] InvestControlContext context)
        {
            var corretoras = context.Set<Corretora>().ToList();
            return Ok(corretoras);
        }
        
        [HttpGet]
        [Route("obter-eventos")]
        public IActionResult ObterTodosOsEventos([FromServices] InvestControlContext context)
        {
            var eventos = context.Set<Evento>().ToList();
            return Ok(eventos);
        }
        
        [HttpGet]
        [Route("obter-transacoes")]
        public IActionResult ObterTodasAsTransacoes([FromServices] InvestControlContext context)
        {
            var transacoes = context.Set<Transacao>().ToList();
            return Ok(transacoes);
        }        
    }
    
}