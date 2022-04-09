using System.Linq;
using InvestControl.Domain.Entity;
using InvestControl.Infra.Context;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromServices] InvestControlContext context)
        {
            var eventos = context.Set<Evento>().ToList();
            return Ok(eventos);
        }
    }
}