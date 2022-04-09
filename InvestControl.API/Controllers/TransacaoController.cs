using InvestControl.Domain.Entity;
using InvestControl.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/transacao")]
    public class TransacaoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromServices] InvestControlContext context)
        {
            var transacoes = context.Set<Transacao>().ToList();
            return Ok(transacoes);
        }        
    }
}
