using InvestControl.Domain.Entity;
using InvestControl.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/corretora")]
    public class CorretoraController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromServices] InvestControlContext context)
        {
            var corretoras = context.Set<Corretora>().ToList();
            return Ok(corretoras);
        }
    }
}
